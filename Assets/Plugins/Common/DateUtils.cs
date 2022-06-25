using System;

namespace Plugins.Common
{
    public class DateUtils
    {
        public const string DATE_PATTERN = "yyyy-MM-dd";
        public const string DATE_PATTERN_SIMPLE = "yyyyMMdd";
        public const string LOG_DATE_PATTERN = "dd/MMM/yyyy";
        public const string HOUR_PATTERN = "yyyy-MM-dd-HH";
        public const string MIN_PATTERN = "yyyy-MM-dd-HH-mm";
        public const string SIMPLE_SECOND_PATTERN = "yyyyMMddHHmmss";
        public const string COMMON_PATTERN = "yyyy-MM-dd HH:mm:ss";
        public const string TIME_PATTERN = "HH:mm:ss";
        public const string TIME_MIN_PATTERN = "HH:mm";

        public static readonly TimeZoneInfo Us
            = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        // TimeZoneInfo. Create (...) can be used to build your own time zone

        // The following two formats are not understood
        public const string CHART_DATE_PATTERN = "%Y-%m-%d";
        public const string CHART_HOUR_PATTERN = "%Y-%m-%d-%H";

        private const int ONE = 1;

        public static long YesterdayBeginTime()
        {
            DateTime date = DateTime.Now;
            date = date.AddDays(-1);
            // date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
            // If it's just to get the date part, you can get it directly.
            date = date.Date;

            // Return date. Ticks; // in 100 nanoseconds, since January 1, 2001
            // Return date.Ticks/10000;//ms, since 1 January 2001
            return ToJavaMilliseconds(date);
        }

        public static long ToJavaMilliseconds(DateTime value, TimeZoneInfo timezone = null)
        {
            DateTime date1970 = new DateTime(1970, 1, 1, 0, 0, 0);
            date1970 = TimeZoneInfo.ConvertTimeFromUtc(date1970, timezone ?? TimeZoneInfo.Local);
            return (value.Ticks - date1970.Ticks) / 10000;
        }
    }
}