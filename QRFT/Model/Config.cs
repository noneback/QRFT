using QRFT.Utilities;
using System;
using System.IO;
/**
* Config  Class is  Singleton.Each qrtf program only have the only Config to store about all the config.
* Data:
*      infomation about local : files path ,store path,zip or not zip,the hash code for url,and local ipv4 addr 
*      information for itself to use or display : DownloadUrl,UploadUrl
*      
* Usage:
*      var config=Config.getInstance() 
*/

// singleton
namespace QRFT.Model {

    public class Config {
        private string _filePath;
        private string _storePath = "./";
        private static Config config;


        private Config() {
        }

        public string LANAddr { set; get; } = "localhost";

        public int Port { set; get; } = 5000;

        public string BaseURL { get => $"http://{LANAddr}:{Port}/api/file/"; }

        public string UploadURL { get => $"{BaseURL}upload"; }

        public string DownloadURL { get => $"{BaseURL}{Hash}"; }

        public string FilePath {
            set {
                //should detect whether file exists
                _filePath = value;
                Hash = Utils.Hash(value);
            }
            get => _filePath;
        }

        public string Hash { get; set; }

        public string StorePath {
            get => _storePath;
            set {
                if (!Directory.Exists(value) ) {
                    Logger.Error("Store should be a folder\n");
                    Environment.Exit(1);
                }
                _storePath = value;
            }
        }

        public static Config GetInstance() {
            if (config == null) {
                config = new Config();
            }
            return config;
        }
    }
}