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
            try {
                config.Port = Utils.GetRandomPort();
                config.LANAddr = Utils.GetLocalIp();
          
                if (args == null || args.Length == 0) {
                    Logger.Error("args missing or Error\n");
                    args = new string[] { "--help" };
                }


                var state = Parser.Default.ParseArguments<SendOptions, ReceiveOptions>(args).MapResult(
                    (SendOptions o) => ArgsParser.SendSolution(o),
                    (ReceiveOptions o) => ArgsParser.ReceiveSolution(o),
                    error => 1
                    );
                if (state != 0) Environment.Exit(1);

                if (args[0].Equals("--help"))
                    Environment.Exit(1);
                CreateHostBuilder(new string[] { "--urls", $"http://*:{config.Port}" }).Build().Run();
            } catch (Exception e) {
                Logger.Error($"internal error occured:{e.Message}");
            }
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });

    }

}