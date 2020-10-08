using System;

namespace QRFT.Utilities {

    public class Logger {

        public static void consoleRouterLog(string method, string router, DateTime dateTime) {
            Console.WriteLine($"{{\n\tMethod:{method}\n\tRouter:{router}\n\tDate:{dateTime.ToLongDateString()}  {dateTime.ToLongTimeString()}\n}}");
        }
    }
}