using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;

namespace QRFT.Utilities {
    //todo pathHash,ZipFiles

    public static class Utils {
        private static Dictionary<string, string> map = new Dictionary<string, string>();
        private const string zipPath = "./tmp.zip";

        public static string hash(string filePath) {
            var key = new Random().Next(1000, 10000).ToString();
            map.Add(key, filePath);
            return key;
        }

        public static string getFilePath(string hash) {
            return map[hash];
        }

        public static bool createZipFiles(params string[] filePaths) {
            try {
                //create an empty zip

                ZipFile.CreateFromDirectory("./NULL", zipPath);
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
            string hostname = Dns.GetHostName();//得到本机名   
            IPHostEntry localhost = Dns.GetHostEntry(hostname);
            List<IPAddress> ipv4 = new List<IPAddress>();
            foreach (var addr in localhost.AddressList) {
                if (addr.AddressFamily == AddressFamily.InterNetwork) {
                    ipv4.Add(addr);
                    Console.WriteLine(addr.ToString());
                }

            }
            return ipv4[ipv4.Count-1].ToString();
            
        }
    }
}