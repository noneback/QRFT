using QRFT.Utilities;
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
        private static Config config;

        private Config() {
        }

        public string LANAddr { set; get; } = "localhost";

        public int Port { set; get; } = 5000;

        public string BaseURL { get =>$"http://{LANAddr}:{Port}/api/file/";}

        public string UploadURL { get => $"http://{LANAddr}:{Port}/api/file/"; }

        public string DownloadURL {   get => $"http://{LANAddr}:{Port}/api/file/{Hash}";}

        public string FilePath {
            set {
                //should detect whether file exists
                _filePath = value;
                Hash = Utils.hash(value);
            }
            get => _filePath;
        }

        public string Hash {get;set;}

        public static Config getInstance() {
            if (config == null) {
                config = new Config();
            }
            return config;
        }
    }
}