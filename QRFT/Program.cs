using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using QRFT.Model;
using QRFT.Utilities;
using CommandLine;
using System;




namespace QRTF {

    public class Program {
        private static Config config = Config.GetInstance();

        public static void Main(string[] args) {

            Console.WriteLine("QRCP in terminal");
            config.Port = Utils.GetRandomPort();
            if (args == null || args.Length == 0) 
                args = new string[] { "--help" };
            else {
                Console.WriteLine("args missing\n");
            }

            Parser.Default.ParseArguments<SendOptions, ReceiveOptions>(args).MapResult(
                (SendOptions o) => ArgsParser.SendSolution(o),
                (ReceiveOptions o) => ArgsParser.ReceiveSolution(o),
                error=>1
                ); 





            // for t
            //config.FilePath = "./SAVE/test.txt";
            //Console.WriteLine(config.Hash);
            //config.LANAddr = Utils.getLocalIp();
            //Console.WriteLine(config.DownloadURL);
            //Console.WriteLine(Utils.generateQRCode(config.DownloadURL));

            //test zip
            //Utils.createZipFiles("./Program.cs", "./Startup.cs");
            //Utils.deleteTmpZipFIle();
            //parse and save into config
            if (args[0].Equals("--help"))
                Environment.Exit(-1);


            CreateHostBuilder(new string[] { "--urls", $"http://*:{config.Port}"}).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });

    }

}