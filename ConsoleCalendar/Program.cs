using System;

namespace ConsoleCalendar {
    internal static class Program {
        public static void Main(string[] args) {
            var showHolidays = Array.Exists(args, s => s.Equals("--show"));
            var holidaysAreWeekends = showHolidays && Array.Exists(args, s => s.Equals("--weekends"));

            Console.WriteLine("Welcome to ICalendar, please, type a date you want to see");
            var dateString = Console.ReadLine();
            DateTime date;
            if (DateTime.TryParse(dateString, out date)) {
                MonthGenerator.PrintMonth(date, showHolidays, holidaysAreWeekends);
            }
            else {
                Console.WriteLine("Sorry, date format is incorrect...");
            }
        }
    }
}