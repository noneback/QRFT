using QRFT.Utilities;

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
                //DownloadURL = "TEST";
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