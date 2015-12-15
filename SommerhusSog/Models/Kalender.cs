using System;
using System.Globalization;

namespace SommerhusSog.Models
{
    public class Kalender
    {
        // Check i kalender dictionairy om givne dato er ledig.
        public static Boolean ErLedig(Hus hus, int uge, int aar)
        {
            return !hus.HusKalender.ContainsKey(aar + "/" + uge);
        }
        // Booking funktionalitet, Hvis ledig: Book retunér True, ellers false.
        public static Boolean Book(Hus hus, int uge, int aar, Lejer lejer)
        {
            if (ErLedig(hus, uge, aar))
            {
                hus.HusKalender.Add(aar + "/" + uge, lejer);
                KollektionHus.GemAlle();
                return true;
            }
            return false;
        }
        // Hvilket år ?
        public static int GetYear()
        {
            return CultureInfo.InvariantCulture.Calendar.GetYear(DateTime.Now);
        }

        // Find hvor mange uger der findes i år. 
        public static int GetWeeksInYear(int year)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date1 = new DateTime(year, 12, 31);
            try {
                Calendar cal = dfi.Calendar;
                return cal.GetWeekOfYear(date1, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            }
            catch (ArgumentOutOfRangeException) {
                return 51;
            }
        }

        public static int GetWeekOfYear()
        {
            DateTime time = DateTime.Now;
        
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

    }
}