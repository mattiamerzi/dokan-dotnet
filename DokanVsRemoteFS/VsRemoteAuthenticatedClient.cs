using Grpc.Net.Client;
using VsRemote;
using static VsRemote.VsRemote;

namespace DokanVsRemoteFS;

internal class VsRemoteAuthenticatedClient: VsRemoteClient
{
    private readonly string authToken;

    public VsRemoteAuthenticatedClient(GrpcChannel channel, string username, string password): base(channel)
    {
        var res = Login(new LoginRequest() { Username = username, Password = password });
        if (res.AuthResult == AuthResult.Authenticated)
        {
            authToken = res.AuthToken;
        }
        else
        {
            throw new VsRemoteAuthenticationException(res.FailureMessage);
        }
    }

    public CreateDirectoryResponse CreateDirectory(CreateDirectoryRequest request)
    {
        request.AuthToken = authToken;
        return base.CreateDirectory(request);
    }
    public WriteFileResponse CreateFile(CreateFileRequest request)
    {
        request.AuthToken = authToken;
        return base.CreateFile(request);
    }
    public DeleteFileResponse DeleteFile(DeleteFileRequest request)
    {
        request.AuthToken = authToken;
        return base.DeleteFile(request);
    }
    public ExecuteCommandResponse ExecuteCommand(ExecuteCommandRequest request)
    {
        request.AuthToken = authToken;
        return base.ExecuteCommand(request);
    }
    public ListCommandsResponse ListCommands(ListCommandsRequest request)
    {
        request.AuthToken = authToken;
        return base.ListCommands(request);
    }
    public ListDirectoryResponse ListDirectory(ListDirectoryRequest request)
    {
        request.AuthToken = authToken;
        return base.ListDirectory(request);
    }
    public ReadFileResponse ReadFile(ReadFileRequest request)
    {
        request.AuthToken = authToken;
        return base.ReadFile(request);
    }
    public ReadFileResponse ReadFileOffset(ReadFileOffsetRequest request)
    {
        request.AuthToken = authToken;
        return base.ReadFileOffset(request);
    }
    public RemoveDirectoryResponse RemoveDirectory(RemoveDirectoryRequest request)
    {
        request.AuthToken = authToken;
        return base.RemoveDirectory(request);
    }
    public RenameFileResponse RenameFile(RenameFileRequest request)
    {
        request.AuthToken = authToken;
        return base.RenameFile(request);
    }
    public StatResponse Stat(StatRequest request)
    {
        request.AuthToken = authToken;
        return base.Stat(request);
    }
    public WriteFileResponse WriteFile(WriteFileRequest request)
    {
        request.AuthToken = authToken;
        return base.WriteFile(request);
    }
    public WriteFileResponse WriteFileAppend(WriteFileAppendRequest request)
    {
        request.AuthToken = authToken;
        return base.WriteFileAppend(request);
    }
    public WriteFileResponse WriteFileOffset(WriteFileOffsetRequest request)
    {
        request.AuthToken = authToken;
        return base.WriteFileOffset(request);
    }
}
