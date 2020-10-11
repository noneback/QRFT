using CommandLine;
using QRFT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QRFT.Model {


    [Verb("send", HelpText = "send file to other terminals")]
    public class SendOptions {
        [Option('z', "zip", Required = false, HelpText = "create zip archive to transfer")]
        public bool IsZip { get; set; }

        [Option('r', "romote", Required = false, HelpText = "send to remote")]
        public bool IsRemote { get; set; }

        [Value(0, HelpText = "files to be transfered")]
        public IEnumerable<string> Files { get; set; }
    }

    [Verb("receive", HelpText = "receive from other terminals")]
    public class ReceiveOptions {
        [Value(0, HelpText = "store path of received file")]
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
            Console.WriteLine("receive mode", options.StorePath);
            config.StorePath = options.StorePath;
            config.LANAddr = Utils.GetLocalIp();
            Console.WriteLine(config.UploadURL);
            Console.WriteLine(Utils.GenerateQRCode(config.UploadURL));
            return 0;
        }

        public static int SendSolution(SendOptions options) {
            Console.WriteLine("sending mode");
            if (options.IsZip) {
                Utils.CreateZipFiles(options.Files.ToArray());
                config.FilePath = Utils.ZipFile;
            } else {
                config.FilePath = options.Files.ToArray()[0];
            }
            config.Hash = Utils.Hash(config.FilePath);
            config.LANAddr = Utils.GetLocalIp();
            Console.WriteLine(config.DownloadURL);
            Console.WriteLine(Utils.GenerateQRCode(config.DownloadURL));

            return 0;
        }
    }

}
