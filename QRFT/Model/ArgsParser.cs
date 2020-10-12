using CommandLine;
using QRFT.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QRFT.Model {


        [Verb("send", HelpText = "Send file to other terminals")]
    public class SendOptions {
        [Option('z', "zip", Required = false, HelpText = "Create zip archive to transfer")]
        public bool IsZip { get; set; }

        [Option('r', "romote", Required = false, HelpText = "Send to remote")]
        public bool IsRemote { get; set; }

        [Value(0, HelpText = "Files to be transfered")]
        public IEnumerable<string> Files { get; set; }
    }

    [Verb("receive", HelpText = "Receive from other terminals")]
    public class ReceiveOptions {
        [Value(0, HelpText = "Store path of received file")]
        public string StorePath { get; set; }
    }


    public class ArgsParser {
        private static ArgsParser parser;
        private static Config config = Config.GetInstance();

        public static ArgsParser GetInstance() {
            if (parser == null) parser = new ArgsParser();
            return parser;
        }
        public static int ReceiveSolution(ReceiveOptions options) {
            var storePath = options.StorePath;
            if (storePath == null || storePath.Length == 0 || !Directory.Exists(storePath)) {
                Logger.Error("store path  missing or is not a Directory");
                return 1;
            }
            config.StorePath = storePath;
            Console.WriteLine(config.UploadURL);
            Console.WriteLine(Utils.GenerateQRCode(config.UploadURL));
            Console.WriteLine("Ctrl+c to exit");
            return 0;
        }

        public static int SendSolution(SendOptions options) {
            var files = options.Files.ToArray();

            if (options.Files == null || files.Length == 0) {
                Logger.Error("Files name missing");
                return 1;
            }
            foreach (var file in files) {
                if (!File.Exists(file)) {
                    Logger.Error($"File not exists{file}");
                    return 1;
                }
            }
            if (options.IsZip) {
                Utils.CreateZipFiles(files);
                config.FilePath = Utils.ZipFile;
            } else {
                config.FilePath = files[0];
            }
            config.Hash = Utils.Hash(config.FilePath);
            Console.WriteLine(config.DownloadURL);
            Console.WriteLine(Utils.GenerateQRCode(config.DownloadURL));
            Console.WriteLine("Ctrl+c to exit");
            return 0;
        }
    }

}
