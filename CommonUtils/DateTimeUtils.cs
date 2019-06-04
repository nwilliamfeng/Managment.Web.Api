using System;

namespace CommonUtils
{
    /// <summary>
    /// 时间操作类
    /// </summary>
    public static class DateTimeUtils
    {
        private static DateTime _starttime = new DateTime(1970, 1, 1, 0, 0, 0);

        /// <summary>
        /// unix时间戳，毫秒
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static long ToTimeStamp(DateTime datetime)
        {
            return (long)datetime.ToUniversalTime().Subtract(_starttime).TotalMilliseconds;
        }

        /// <summary>
        /// 将毫秒转换成时间格式
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime FromTimeStamp(long timestamp)
        {
            return _starttime.AddMilliseconds(timestamp).ToLocalTime();
        }


        /// <summary>
        /// 时间转换成星座
        /// </summary>
        /// <param name="birthday"></param>
        /// <returns></returns>
        public static string GetAtomFromBirthday(DateTime birthday)
        {
            float birthdayF = 0.00F;

            if (birthday.Month == 1 && birthday.Day < 20)
            {
                birthdayF = float.Parse(string.Format("13.{0}", birthday.Day));
            }
            else
            {
                birthdayF = float.Parse(string.Format("{0}.{1}", birthday.Month, birthday.Day));
            }
            float[] atomBound = { 1.20F, 2.20F, 3.21F, 4.21F, 5.21F, 6.22F, 7.23F, 8.23F, 9.23F, 10.23F, 11.21F, 12.22F, 13.20F };
            string[] atoms = { "水瓶座", "双鱼座", "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座", "魔羯座" };

            string ret = "保密";
            for (int i = 0; i < atomBound.Length - 1; i++)
            {
                if (atomBound[i] <= birthdayF && atomBound[i + 1] > birthdayF)
                {
                    ret = atoms[i];
                    break;
                }
            }
            return ret;
        }


        /// <summary>  
        /// 得到本周第几天
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static DateTime GetWeekDay(DateTime datetime, int n)
        {
            //星期一为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - n) : (weeknow - n));
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }

        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToSecond(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000000;   //除10000调整为13位      
            return t;
        }

        /// <summary>        
        /// 时间戳转为C#格式时间        
        /// </summary>          
        /// <returns></returns>        
        public static DateTime ConvertSecondToDateTime(long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return dtStart.AddSeconds(timeStamp);
        }


        /// <summary>
        /// 将yyyyMMddHHmmss 转换成日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTimeByFormat(string dateTime)
        {
            DateTime result = DateTime.MinValue;
            if (!string.IsNullOrEmpty(dateTime))
            {
                result = new DateTime(dateTime.Length >= 4 ? FormatUtils.ConvertToInt32(dateTime.Substring(0, 4), 0) : 2017,
                   dateTime.Length >= 6 ? FormatUtils.ConvertToInt32(dateTime.Substring(4, 2), 0) : 0,
                    dateTime.Length >= 8 ? FormatUtils.ConvertToInt32(dateTime.Substring(6, 2), 0) : 0,
                    dateTime.Length >= 10 ? FormatUtils.ConvertToInt32(dateTime.Substring(8, 2), 0) : 0,
                    dateTime.Length >= 12 ? FormatUtils.ConvertToInt32(dateTime.Substring(10, 2), 0) : 0,
                    dateTime.Length >= 14 ? FormatUtils.ConvertToInt32(dateTime.Substring(12, 2), 0) : 0);
            }
            return result;
        }

        /// <summary>
        /// 将时间转换到unix时间戳,单位秒
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static uint ToUnixTime(this DateTime date)
        {
            var result = (date.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return (uint)result;
        }

    }
}
