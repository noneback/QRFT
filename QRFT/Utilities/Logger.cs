using System;
using System.Linq;

namespace QRFT.Utilities {

    public class Logger {

        public static void ConsoleRouterLog(string method, string router, DateTime dateTime) {
            Console.WriteLine($"{{\n\tMethod:{method}\n\tRouter:{router}\n\tDate:{dateTime.ToLongDateString()}  {dateTime.ToLongTimeString()}\n}}");
        }

        public static void Error(params string[] errMsgs) {
            Console.Error.WriteLine("Error:\n" + String.Join('\n', errMsgs));
        }
    }
}