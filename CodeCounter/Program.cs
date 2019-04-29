using System;
using System.Globalization;

namespace CodeCounter {
    internal static class Program {
        public static void Main(string[] args) {
            
            try {
                CodeStringCounter.Configure("comments.txt");
            }
            catch (Exception) {
                Console.WriteLine("Can't process config file");
                return;
            }

            if (args.Length > 1) {
                Console.WriteLine("Too mny arguments, expecting 0 or 1");
                return;
            }
            var lines = CodeStringCounter.CountStrings(args.Length == 1 ? args[0] : "*");
            Console.WriteLine(lines);
        }
    }
}