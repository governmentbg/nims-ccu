using System;

namespace Eumis.Common
{
    public static class DateTimeExtensions
    {
        public static int ConvertHoursToMilliseconds(this DateTime value)
        {
            return ((value.Hour * 60) + value.Minute) * 60000;
        }

        public static DateTime ToStartOfDay(this DateTime dateTime)
        {
            return new DateTime(
             dateTime.Year,
             dateTime.Month,
             dateTime.Day,
             0,
             0,
             0,
             0);
        }

        public static DateTime ToEndOfDay(this DateTime dateTime)
        {
            return new DateTime(
             dateTime.Year,
             dateTime.Month,
             dateTime.Day,
             23,
             59,
             59,
             999);
        }

        public static DateTime StartOfDay(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        public static DateTime EndOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddTicks(-1);
        }
    }
}
