﻿using System.Runtime.InteropServices;
using System.Security.AccessControl;
using DokanNet;
using Grpc.Core;
using Grpc.Net.Client;
using VsRemote;
using VsRemote.Exceptions;
using FileAccess = DokanNet.FileAccess;
using static VsRemote.VsRemote;
using Google.Protobuf;
using System.Diagnostics.CodeAnalysis;
using DokanNet.Logging;

namespace DokanVsRemoteFS;

internal class VsRemoteFS : IDokanOperations
{
    private const FileAccess DataAccess = FileAccess.ReadData | FileAccess.WriteData | FileAccess.AppendData |
                                          FileAccess.Execute |
                                          FileAccess.GenericExecute | FileAccess.GenericWrite |
                                          FileAccess.GenericRead;

    private const FileAccess DataWriteAccess = FileAccess.WriteData | FileAccess.AppendData |
                                               FileAccess.Delete |
                                               FileAccess.GenericWrite;

    GrpcChannel channel;
    VsRemoteClient vsremote;
    private readonly ILogger log;

    public VsRemoteFS(string uri, ILogger logger)
    {
        log = logger;
        channel = GrpcChannel.ForAddress(uri);
        vsremote = new VsRemoteClient(channel);
    }

    private static NtStatus DecodeNtStatus(RpcException rpcex)
        => DecodeNtStatus(rpcex.VsErrorCode());

    private static NtStatus DecodeNtStatus(string vs_error_code)
        => vs_error_code switch
        {
            VsRemote.Exceptions.FileExists.ERROR_CODE => DokanResult.AlreadyExists,
            InvalidPath.ERROR_CODE => DokanResult.InvalidName,
            IOError.ERROR_CODE => DokanResult.InternalError,
            IsADirectory.ERROR_CODE => DokanResult.Error,
            NotADirectory.ERROR_CODE => DokanResult.NotADirectory,
            NotEmpty.ERROR_CODE => DokanResult.DirectoryNotEmpty,
            NotFound.ERROR_CODE => NtStatus.ObjectNameNotFound,
            PermissionDenied.ERROR_CODE => DokanResult.AccessDenied,
            ServerError.ERROR_CODE => DokanResult.InternalError,
            _ => DokanResult.InternalError,
        };

    private bool FileExists(string fileName, out bool isDirectory, [NotNullWhen(true)] out VsFsEntry? fileInfo)
    {
        try
        {
            var fstat = vsremote.Stat(new StatRequest() { Path = fileName });
            isDirectory = fstat.FileInfo.FileType == FileType.Directory;
            fileInfo = fstat.FileInfo;
            return true;
        }
        catch (RpcException rpcex)
        {
            isDirectory = false;
            fileInfo = null;
            var vs_error_code = rpcex.VsErrorCode();
            if (vs_error_code != NotFound.ERROR_CODE)
                throw;
            else
                return false;
        }
    }
    public NtStatus CreateFile(string fileName, DokanNet.FileAccess access, FileShare share, FileMode mode, FileOptions options, FileAttributes attributes, IDokanFileInfo info)
    {
        StatRequest statRequest = new()
        {
            Path = fileName
        };
        StatResponse? stat = null;
        try
        {
            stat = vsremote.Stat(statRequest);
        }
        catch (RpcException rpcex)
        {
            var vs_error_code = rpcex.VsErrorCode();
            if (vs_error_code != NotFound.ERROR_CODE)
                return DecodeNtStatus(vs_error_code);
        }
        VsFsEntry? entry = stat?.FileInfo;
        var pathExists = entry != null;
        var pathIsDirectory = pathExists && entry!.FileType == FileType.Directory;
        var result = DokanResult.Success;

        try
        {
            if (info.IsDirectory)
            {
                switch (mode)
                {
                    case FileMode.Open:
                        if (pathExists && !pathIsDirectory)
                        {
                            return DokanResult.NotADirectory;
                        }
                        if (!pathExists)
                            return DokanResult.FileNotFound;

                        // test: directory access
                        vsremote.ListDirectory(new ListDirectoryRequest() { Path = fileName });
                        break;

                    case FileMode.CreateNew:
                        if (entry != null && !pathIsDirectory)
                            return DokanResult.FileExists;

                        if (entry != null)
                            return DokanResult.AlreadyExists;

                        vsremote.CreateDirectory(new CreateDirectoryRequest() { Path = fileName });
                        break;
                }
            }
            else
            {
                var readWriteAttributes = (access & DataAccess) == 0;
                //var readAccess = (access & DataWriteAccess) == 0;

                switch (mode)
                {
                    case FileMode.Open:

                        if (pathExists)
                        {
                            // check if driver only wants to read attributes, security info, or open directory
                            if (readWriteAttributes || pathIsDirectory)
                            {
                                if (pathIsDirectory && (access & FileAccess.Delete) == FileAccess.Delete
                                    && (access & FileAccess.Synchronize) != FileAccess.Synchronize)
                                    //It is a DeleteFile request on a directory
                                    return DokanResult.AccessDenied;

                                info.IsDirectory = pathIsDirectory;
                            }
                        }
                        else
                        {
                            return DokanResult.FileNotFound;
                        }
                        break;

                    case FileMode.CreateNew:
                        if (pathExists)
                            return DokanResult.FileExists;
                        vsremote.CreateFile(new CreateFileRequest() { Path = fileName });
                        break;

                    case FileMode.Truncate:
                        if (!pathExists)
                            return DokanResult.FileNotFound;
                        vsremote.WriteFile(new WriteFileRequest() { Path = fileName, Content = Google.Protobuf.ByteString.Empty, Create = false, Overwrite = true });
                        break;

                    case FileMode.Create:
                    case FileMode.OpenOrCreate:
                        if (pathExists)
                            return DokanResult.AlreadyExists;
                        break;

                    case FileMode.Append:
                        if (!pathExists)
                            return DokanResult.FileNotFound;
                        break;
                }
            }
        }
        catch (RpcException rpcex)
        {
            return DecodeNtStatus(rpcex);
        }
        catch (Exception ex)
        {
            throw;
        }
        // must set it to something if you return DokanError.Success
        if (result == DokanResult.Success)
            info.Context = new object();
        return result;
    }

// ------------------------------------------------------------------------------------------------------------------------

    public void Cleanup(string fileName, IDokanFileInfo info)
    {
        if (info.DeleteOnClose)
        {
            if (info.IsDirectory)
            {
                vsremote.RemoveDirectory(new RemoveDirectoryRequest() { Path = fileName, Recursive = false });
            }
            else
            {
                vsremote.DeleteFile(new DeleteFileRequest() { Path = fileName });
            }
        }
    }

    public void CloseFile(string fileName, IDokanFileInfo info)
    {
    }

    public NtStatus ReadFile(string fileName, byte[] buffer, out int bytesRead, long offset, IDokanFileInfo info)
    {
        var readfileres = vsremote.ReadFileOffset(new ReadFileOffsetRequest() { Path = fileName, Offset = Convert.ToInt32(offset), Length = buffer.Length });
        bytesRead = Math.Min(readfileres.Length, buffer.Length);
        Array.Copy(readfileres.Content.ToByteArray(), buffer, bytesRead);
        return NtStatus.Success;
    }

    public NtStatus WriteFile(string fileName, byte[] buffer, out int bytesWritten, long offset, IDokanFileInfo info)
    {
        var append = offset == -1;
        if (append)
        {
            vsremote.WriteFileAppend(new WriteFileAppendRequest() { Path = fileName, Content = ByteString.CopyFrom(buffer) });
        }
        else
        {
            vsremote.WriteFileOffset(new WriteFileOffsetRequest() { Path = fileName, Content = ByteString.CopyFrom(buffer), Offset = Convert.ToInt32(offset) });
        }
        bytesWritten = buffer.Length;
        return DokanResult.Success;
    }

    public NtStatus FlushFileBuffers(string fileName, IDokanFileInfo info)
    {
        return DokanResult.Success;
    }

    private static DateTime? Unix(long unixts)
        => unixts > 0 ? DateTimeOffset.FromUnixTimeSeconds(unixts).LocalDateTime : null ;

    public NtStatus GetFileInformation(string fileName, out FileInformation fileInfo, IDokanFileInfo info)
    {
        var stat = vsremote.Stat(new StatRequest() { Path = fileName });
        var finfo = stat.FileInfo;
        fileInfo = new FileInformation
        {
            FileName = fileName,
            Attributes =
                (finfo.FileType == FileType.Directory ? FileAttributes.Directory : FileAttributes.Normal) |
                (finfo.Readonly ? FileAttributes.ReadOnly : FileAttributes.None),
            CreationTime = Unix(finfo.Ctime),
            LastWriteTime = Unix(finfo.Mtime),
            LastAccessTime = Unix(finfo.Atime),
            Length = finfo.Size
        };
        return DokanResult.Success;
    }

    public NtStatus SetFileAttributes(string fileName, FileAttributes attributes, IDokanFileInfo info)
        => DokanResult.Success;

    public NtStatus SetFileTime(string fileName, DateTime? creationTime, DateTime? lastAccessTime, DateTime? lastWriteTime, IDokanFileInfo info)
        => DokanResult.Success; // we do not change file times; does this work?

    public NtStatus DeleteFile(string fileName, IDokanFileInfo info)
    {
        var fileExists = FileExists(fileName, out bool isDirectory, out VsFsEntry? fileInfo);
        if (isDirectory)
            return DokanResult.AccessDenied;
        if (!fileExists)
            return DokanResult.FileNotFound;
        return DokanResult.Success;
    }

    public NtStatus DeleteDirectory(string fileName, IDokanFileInfo info)
    {
        try
        {
            vsremote.RemoveDirectory(new RemoveDirectoryRequest() { Path = fileName, Recursive = false });
            return DokanResult.Success;
        }
        catch (RpcException rpcex)
        {
            if (rpcex.VsErrorCode() == NotEmpty.ERROR_CODE)
                return DokanResult.DirectoryNotEmpty;
            else
                throw;
        }
    }

    public NtStatus MoveFile(string oldName, string newName, bool replace, IDokanFileInfo info)
    {
        var oldFileExists = FileExists(oldName, out bool oldIsDirectory, out VsFsEntry? oldFileInfo);
        var newFileExists = FileExists(oldName, out bool newIsDirectory, out VsFsEntry? newFileInfo);

        if (!newFileExists)
        {
            vsremote.RenameFile(new RenameFileRequest() { FromPath = oldName, ToPath = newName, Overwrite = replace });
        }
        else
        {
            if (replace)
            {
                if (newIsDirectory)
                    return DokanResult.AccessDenied;
                vsremote.RenameFile(new RenameFileRequest() { FromPath = oldName, ToPath = newName, Overwrite = replace });
            }
        }


        return DokanResult.Success;
    }
    public NtStatus SetAllocationSize(string fileName, long length, IDokanFileInfo info)
    {
        if (FileExists(fileName, out bool isDirectory, out VsFsEntry? fileInfo))
        {
            if (fileInfo.Size != length)
            {
                byte[] file = vsremote.ReadFile(new ReadFileRequest() { Path = fileName }).ToByteArray();
                Array.Resize(ref file, Convert.ToInt32(length));
                vsremote.WriteFile(new WriteFileRequest() { Path = fileName, Content = ByteString.CopyFrom(file), Create = false, Overwrite = true });
            }
            return DokanResult.Success;
        }
        else
        {
            return DokanResult.FileNotFound;
        }
    }

    public NtStatus SetEndOfFile(string fileName, long length, IDokanFileInfo info)
        => SetAllocationSize(fileName, length, info);

    public NtStatus LockFile(string fileName, long offset, long length, IDokanFileInfo info)
        => DokanResult.Success;

    public NtStatus UnlockFile(string fileName, long offset, long length, IDokanFileInfo info)
        => DokanResult.Success;

    public NtStatus GetDiskFreeSpace(out long freeBytesAvailable, out long totalNumberOfBytes, out long totalNumberOfFreeBytes, IDokanFileInfo info)
    {
        freeBytesAvailable = 1024_000_000;
        totalNumberOfBytes = 1024_000_000;
        totalNumberOfFreeBytes = 512_000_000;
        return DokanResult.Success;
    }

    public NtStatus GetVolumeInformation(out string volumeLabel, out FileSystemFeatures features, out string fileSystemName, out uint maximumComponentLength, IDokanFileInfo info)
    {
        volumeLabel = "VsRemote";
        fileSystemName = "NTFS";
        maximumComponentLength = 256;

        features = FileSystemFeatures.CasePreservedNames | FileSystemFeatures.CaseSensitiveSearch |
                   FileSystemFeatures.SupportsRemoteStorage | FileSystemFeatures.UnicodeOnDisk;

        return DokanResult.Success;
    }

    public NtStatus GetFileSecurity(string fileName, out FileSystemSecurity security, AccessControlSections sections, IDokanFileInfo info)
    {
        var fileExists = FileExists(fileName, out bool isDirectory, out VsFsEntry? fileInfo);
        if (fileExists && isDirectory)
        {
            security = new DirectorySecurity(fileName, AccessControlSections.None);
        }
        security = new FileSecurity(fileName, AccessControlSections.None);
        return fileExists ? DokanResult.Success : DokanResult.FileNotFound;
    }

    public NtStatus SetFileSecurity(string fileName, FileSystemSecurity security, AccessControlSections sections, IDokanFileInfo info)
    {
        return DokanResult.Success;
    }

    public NtStatus Mounted(string mountPoint, IDokanFileInfo info)
    {
        return DokanResult.Success;
    }

    public NtStatus Unmounted(IDokanFileInfo info)
    {
        return DokanResult.Success;
    }

    public NtStatus FindStreams(string fileName, IntPtr enumContext, out string streamName, out long streamSize,
        IDokanFileInfo info)
    {
        streamName = string.Empty;
        streamSize = 0;
        return DokanResult.NotImplemented;
    }

    public NtStatus FindStreams(string fileName, out IList<FileInformation> streams, IDokanFileInfo info)
    {
        streams = [];
        return DokanResult.NotImplemented;
    }

    public IList<FileInformation> FindFilesHelper(string fileName, string searchPattern, IList<VsFsEntry> allFiles)
    {
        var files = allFiles
            .Where(finfo => DokanHelper.DokanIsNameInExpression(searchPattern, finfo.Name, true))
            .Select(finfo => new FileInformation
            {
                Attributes = 
                    (finfo.FileType == FileType.Directory ? FileAttributes.Directory : FileAttributes.Normal) |
                    (finfo.Readonly ? FileAttributes.ReadOnly : FileAttributes.None),
                CreationTime = Unix(finfo.Ctime),
                LastAccessTime = Unix(finfo.Atime),
                LastWriteTime = Unix(finfo.Mtime),
                Length = finfo.Size,
                FileName = finfo.Name
            }).ToArray();

        return files;
    }

    public NtStatus FindFilesWithPattern(string fileName, string searchPattern, out IList<FileInformation> files, IDokanFileInfo info)
    {
        var listdirres = vsremote.ListDirectory(new ListDirectoryRequest() { Path = fileName });
        files = FindFilesHelper(fileName, searchPattern, listdirres.Entries);

        return DokanResult.Success;
    }

    public NtStatus FindFiles(string fileName, out IList<FileInformation> files, IDokanFileInfo info)
    {
        var listdirres = vsremote.ListDirectory(new ListDirectoryRequest() { Path = fileName });

        // This function is not called because FindFilesWithPattern is implemented
        // Return DokanResult.NotImplemented in FindFilesWithPattern to make FindFiles called
        files = FindFilesHelper(fileName, "*", listdirres.Entries);

        return DokanResult.Success;
    }

}
