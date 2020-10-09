using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;

/**
 * Utils has all the other function that should be used in program
 * 
 */

namespace QRFT.Utilities {
    //todo pathHash,ZipFiles

    public static class Utils {

        private static Dictionary<string, string> map = new Dictionary<string, string>();// for path hash
        private static string zipPath = $"./tmp-{DateTime.Now.ToLongDateString()}-{new Random().Next(100, 1000).ToString()}.zip";
        private static string tmpNullDirPath = "./.null";

        public static string hash(string filePath) {
            //path hash
            var key = new Random().Next(1000, 10000).ToString();
            map.Add(key, filePath);
            return key;
        }

        public static string getFilePath(string hash) {
            //get path 
            return map[hash];
        }

        public static bool createZipFiles(params string[] filePaths) {
            try {
                //create an empty zip
                if (!Directory.Exists(tmpNullDirPath)) {
                    Directory.CreateDirectory(tmpNullDirPath);
                }
                ZipFile.CreateFromDirectory(tmpNullDirPath, zipPath);

                // todo this zipPath should be a paras pass by function
                Directory.Delete(tmpNullDirPath);
                // add files init

                using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Open)) {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update)) {
                        foreach (var filepath in filePaths) {
                            ZipArchiveEntry entry = archive.CreateEntry(filepath);

                            using (StreamWriter writer = new StreamWriter(entry.Open())) {
                                var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                                stream.CopyTo(writer.BaseStream);
                            }
                        }
                    }
                }
                Console.WriteLine("zip archive has been created\n");
                return true;
            } catch (FileNotFoundException e) {
                Console.Error.WriteLine("File Not found: ", e.FileName);
            } catch (IOException e) {
                Console.Error.WriteLine("File has already existed \n", e.Message);
            }
            return false;
        }

        public static bool deleteTmpZipFIle() {
            try {
                File.Delete(zipPath);
                return true;
            } catch (IOException e) {
                Console.Error.WriteLine("Delete tmp.zip failed\n", e.Message);
                return false;
            }
        }

        public static int getBufferSize(long size) {
            //auto set buffersize
            const long G = 1024 * 1024 * 1024;
            const int M = 1024 * 1024;
            const int K = 1024;
            int buffer_size = K * 200;

            if (size >= G * 10) {
                //size>=10G
                buffer_size = 10 * M;
            } else if (size >= G) {
                buffer_size = 5 * M;
            } else if (size >= 500 * M) {
                buffer_size = 3 * M;
            } else if (size >= 100 * M) {
                buffer_size = M;
            } else if (size >= 10 * M) {
                buffer_size = K * 500;
            }

            return buffer_size;
        }

        public static string generateQRCode(string msg) {
            //generate QRCode String
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(msg, QRCodeGenerator.ECCLevel.Q);
            AsciiQRCode qrCode = new AsciiQRCode(qrCodeData);
            return qrCode.GetGraphic(1);
        }
        public static string getLocalIp() {
            //get local ipv4 LAN addr
            string hostname = Dns.GetHostName();//得到本机名   
            IPHostEntry localhost = Dns.GetHostEntry(hostname);
            List<IPAddress> ipv4 = new List<IPAddress>();
            foreach (var addr in localhost.AddressList) {
                if (addr.AddressFamily == AddressFamily.InterNetwork) {
                    ipv4.Add(addr);
                    Console.WriteLine(addr.ToString());
                }

            }
            return ipv4[ipv4.Count - 1].ToString();

        }
    }
}