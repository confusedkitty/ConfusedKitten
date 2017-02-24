using System;
using System.Globalization;

namespace ConfusedKitten.Common
{
    /// <summary>
    ///  时间类
    /// </summary>
    public static class TimeHelper
    {
        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static long ConvertDateTimeInt(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var intResult = (time - startTime).TotalSeconds;
            return (long)intResult;
        }

        /// <summary>
        /// UnixTime
        /// </summary>
        /// <param name="timeStamp">时间数</param>
        /// <returns></returns>
        public static DateTime UnixTime(string timeStamp)
        {
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            try
            {
                string str = timeStamp.PadRight(13, '0');
                long d = Convert.ToInt64(str);
                TimeSpan toNow = new TimeSpan(d);
                time = time.Add(toNow);
            }
            catch
            {
                return time;
            }
            return time;
        }

        /// <summary>
        ///  将当前时间转换为“yyyy-MM-dd HH:mm:ss”格式
        /// </summary>
        /// <returns></returns>
        public static string NowToHms()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        ///  将当前时间转换为“yyyy-MM-dd”格式
        /// </summary>
        /// <returns></returns>
        public static string NowToDay()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        ///  将当前时间转换为“yyyy-MM-dd”格式
        /// </summary>
        /// <returns></returns>
        public static string TimeToDay(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        ///  将MM/dd/yyyy HH:mm:ss 格式转换成正常格式
        /// </summary>
        /// <param name="time">待转换时间</param>
        /// <returns></returns>
        public static DateTime MdyConvert(string time)
        {
            try
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                DateTime text = DateTime.ParseExact(time, "MM/dd/yyyy HH:mm:ss", culture);
                return text;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        ///  将MO31OCT16  星期 日 月 年 格式转换成正常格式
        /// </summary>
        /// <param name="time">待转换时间</param>
        /// <returns></returns>
        public static DateTime WeekDmyConvert(string time)
        {
            try
            {
                CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
                string format = "dMMMyy HHmm";
                DateTime dtTemp = DateTime.ParseExact(time, format, cultureInfo);
                return dtTemp;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        ///  将Date(1420335269000+0800) 格式转换成正常格式
        /// </summary>
        /// <param name="time">待转换时间</param>
        /// <returns></returns>
        public static DateTime TimeStampPlusConvert(string time)
        {
            try
            {
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                time = time.Substring(6);
                long lstrDatetime =
                     Convert.ToInt64(time.Substring(0, time.IndexOf("+", StringComparison.Ordinal)));
                DateTime date = start.AddMilliseconds(lstrDatetime).ToLocalTime();
                return date;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
    }
}
