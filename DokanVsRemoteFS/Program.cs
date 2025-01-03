﻿using System;
using System.Linq;
using System.Threading.Tasks;
using DokanNet;
using DokanNet.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DokanVsRemoteFS
{
    internal class Program
    {
        private const string MirrorKey = "-uri";
        private const string MountKey = "-where";

        private static async Task Main(string[] args)
        {
            try
            {
                var arguments = args
                   .Select(x => x.Split([ '=' ], 2, StringSplitOptions.RemoveEmptyEntries))
                   .ToDictionary(x => x[0], x => x.Length > 1 ? x[1] as object : true, StringComparer.OrdinalIgnoreCase);

                var mirrorPath = arguments.ContainsKey(MirrorKey)
                   ? arguments[MirrorKey] as string
                   : @"http://172.17.0.1:5229";

                var mountPath = arguments.ContainsKey(MountKey)
                   ? arguments[MountKey] as string
                   : @"N:\";

                var dokanLogger = new NullLogger();
                using (var mirrorLogger = new ConsoleLogger("[Mirror] "))
                //using (var dokanLogger = new ConsoleLogger("[Dokan] "))
                using (var dokan = new Dokan(dokanLogger))
                {
                    var mirror = new VsRemoteFS(mirrorPath, mirrorLogger);

                    var dokanBuilder = new DokanInstanceBuilder(dokan)
                        .ConfigureLogger(() => dokanLogger)
                        .ConfigureOptions(options =>
                        {
                            options.Options = DokanOptions.DebugMode | DokanOptions.RemovableDrive;
                            options.MountPoint = mountPath;
                        });
                    using (var dokanInstance = dokanBuilder.Build(mirror))
                    {
                        Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) =>
                        {
                            e.Cancel = true;
                            dokan.RemoveMountPoint(mountPath);
                        };

                        await dokanInstance.WaitForFileSystemClosedAsync(uint.MaxValue);
                    }
                }

                Console.WriteLine(@"Success");
            }
            catch (DokanException ex)
            {
                Console.WriteLine(@"Error: " + ex.Message);
            }
        }
    }
}
