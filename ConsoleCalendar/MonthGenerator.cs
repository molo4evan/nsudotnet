using System;
using System.Linq;

namespace ConsoleCalendar {
    internal static class MonthGenerator {

        private static readonly DateTime[] Holidays = {
            new DateTime(2019, 1, 7), 
            new DateTime(2019, 1, 14),
            new DateTime(2019, 1, 23), 
            new DateTime(2019, 2, 14),
            new DateTime(2019, 2, 23),
            new DateTime(2019, 3, 8),
            new DateTime(2019, 4, 1),
            new DateTime(2019, 5, 1),
            new DateTime(2019, 5, 4),
            new DateTime(2019, 5, 9),
            new DateTime(2019, 6, 1), 
            new DateTime(2019, 6, 12),
            new DateTime(2019,7, 7), 
            new DateTime(2019, 9, 1),
            new DateTime(2019, 10, 5),
            new DateTime(2019, 11, 7), 
            new DateTime(2019, 12, 25),
            new DateTime(2019, 12, 31)
        };
        public static void PrintMonth(DateTime date, bool showHolidays, bool holidaysAreWeekends) {
            var month = date.Month;
            var year = date.Year;
            var daysAmount = DateTime.DaysInMonth(year, month);
            var starterDayOfWeek = new DateTime(year, month, 1).DayOfWeek;
            
            PrintDays(year, month, daysAmount, date.Day, starterDayOfWeek, showHolidays, holidaysAreWeekends);
        }

        private static void PrintDays(int year,
            int month,
            int days,
            int chosenDay,
            DayOfWeek startDay,
            bool showHolidays,
            bool holidaysAreWeekends) {
            var startTab = GetTabulation(startDay);

            var defaultColor = Console.ForegroundColor;
            
            var monthString = GetMonthName(month);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\t{0} {1}" ,monthString, year);
            Console.WriteLine(" Mo  Tu  We  Th  Fr  Sa  Su");
            for (var i = 0; i < startTab; i++) {
                Console.Write("    ");
            }

            var weekends = 0;
            var lastSunday = false;
            
            for (var i = 1; i <= days; i++) {
                var day = new DateTime(year, month, i);
                if (IsWeekend(day.DayOfWeek)) {
                    weekends++;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                else {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (IsHoliday(day)) {
                    if (showHolidays) {
                        Console.ForegroundColor = ConsoleColor.Yellow;                        
                    }
                    if (holidaysAreWeekends) {
                        weekends++;
                    }
                }

                if (i.Equals(chosenDay)) {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }

                Console.Write(i >= 10 ? " {0} " : "  {0} ", i);

                if (day.DayOfWeek.Equals(DayOfWeek.Sunday)) {
                    Console.WriteLine();
                    lastSunday = true;
                }
                else {
                    lastSunday = false;
                }
            }

            var spaces = lastSunday ? "\n" : "\n\n";
            Console.ForegroundColor = ConsoleColor.White;
            var holidaysIncluded = holidaysAreWeekends ? " (including holidays)" : " (only weekends)";
            Console.WriteLine("{0}Working days in this month: {1}{2}", spaces, days - weekends, holidaysIncluded);
            Console.ForegroundColor = defaultColor;
        }

        private static int GetTabulation(DayOfWeek day) {
            int tab;
            switch (day) {
                case DayOfWeek.Monday:
                    tab = 0;
                    break;
                case DayOfWeek.Tuesday:
                    tab = 1;
                    break;
                case DayOfWeek.Wednesday:
                    tab = 2;
                    break;
                case DayOfWeek.Thursday:
                    tab = 3;
                    break;
                case DayOfWeek.Friday:
                    tab = 4;
                    break;
                case DayOfWeek.Saturday:
                    tab = 5;
                    break;
                case DayOfWeek.Sunday:
                    tab = 6;
                    break;
                default:
                    throw new Exception("Should not happen");
            }
            return tab;
        }

        private static bool IsWeekend(DayOfWeek day) {
            return day.Equals(DayOfWeek.Saturday) || day.Equals(DayOfWeek.Sunday);
        }

        private static bool IsHoliday(DateTime day) {
            return Holidays.Any(holiday => holiday.Month.Equals(day.Month) && holiday.Day.Equals(day.Day));
        }

        private static string GetMonthName(int month) {
            string monthName;
            switch (month) {
                case 1:
                    monthName = "January";
                    break;
                case 2:
                    monthName = "February";
                    break;
                case 3:
                    monthName = "March";
                    break;
                case 4:
                    monthName = "April";
                    break;
                case 5:
                    monthName = "May";
                    break;
                case 6:
                    monthName = "June";
                    break;
                case 7:
                    monthName = "July";
                    break;
                case 8:
                    monthName = "August";
                    break;
                case 9:
                    monthName = "September";
                    break;
                case 10:
                    monthName = "October";
                    break;
                case 11:
                    monthName = "November";
                    break;
                case 12:
                    monthName = "December";
                    break;
                default:
                    monthName = "UNKNOWN";
                    break;
            }
            return monthName;
        }
    }
}