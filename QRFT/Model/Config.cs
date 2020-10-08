using QRFT.Utilities;

// singleton
namespace QRFT.Model {

    public class Config {
        private string _filePath;
        private string baseURL = "https://localhost:5001/api/file/";
        private string hash;
        private string downloadUrl;
        private static Config config;

        private Config() {
        }

        public string FilePath {
            set {
                //should detect whether file exists
                _filePath = value;
                Hash = Utils.hash(value);
                //DownloadURL = "TEST";
            }
            get => _filePath;
        }

        public string Hash {
            get => hash;
            set {
                hash = value;
                DownloadURL = baseURL + hash;
            }
        }

        public string DownloadURL {
            get => downloadUrl;
            set => downloadUrl = value;
        }

        public string UploadURL { get; set; }

        public static Config getInstance() {
            if (config == null) {
                config = new Config();
            }
            return config;
        }
    }
}