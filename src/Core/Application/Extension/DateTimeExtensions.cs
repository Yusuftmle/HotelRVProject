using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extension
{
    public static class DateTimeExtensions
    {
        public static string ToFormattedString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string TimeAgo(this DateTime date)
        {
            var diff = DateTime.Now - date;
            if (diff.TotalDays >= 1)
                return $"{(int)diff.TotalDays} gün önce";
            if (diff.TotalHours >= 1)
                return $"{(int)diff.TotalHours} saat önce";
            if (diff.TotalMinutes >= 1)
                return $"{(int)diff.TotalMinutes} dakika önce";
            return "Şimdi";
        }

        public static DateTime AddBusinessDays(this DateTime date, int businessDays)
        {
            var sign = businessDays < 0 ? -1 : 1;
            var days = 0;
            while (businessDays != 0)
            {
                date = date.AddDays(sign);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    businessDays -= sign;
                }
                days++;
            }
            return date;
        }

        public static DateTime StartOfWeek(this DateTime date)
        {
            var dayOfWeek = (int)date.DayOfWeek;
            var startOfWeek = date.AddDays(-dayOfWeek);
            return startOfWeek.Date;
        }

        public static DateTime EndOfWeek(this DateTime date)
        {
            var dayOfWeek = (int)date.DayOfWeek;
            var endOfWeek = date.AddDays(6 - dayOfWeek);
            return endOfWeek.Date.AddDays(1).AddTicks(-1);
        }

    }
}

