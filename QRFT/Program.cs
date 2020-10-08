using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using QRFT.Model;
using System;
using System.Net;

namespace QRTF {

    public class Program {
        private static Config config = Config.getInstance();

        public static void Main(string[] args) {
            Console.WriteLine("QRCP in terminal");
            // for t
            config.FilePath = "./SAVE/test.txt";
            Console.WriteLine(config.Hash);
            Console.WriteLine(config.DownloadURL);
            /*test zip
            Utils.createZipFiles("./Program.cs", "./Startup.cs");
            Utils.deleteTmpZipFIle();*/

            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (var item in ipadrlist) {
                Console.WriteLine(item.ToString());
            }
            //parse and save into config
            foreach (var arg in args) {
                Console.WriteLine(arg);
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}