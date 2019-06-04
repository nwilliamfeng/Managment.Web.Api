using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtils
{
    /// <summary>
    /// 类型转换，格式工具类
    /// </summary>
    public static class FormatUtils
    {
        /// <summary>
        /// 转换bool
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ConvertToBoolean(string input, bool defaultValue)
        {
            bool result = defaultValue;
            if (!String.IsNullOrEmpty(input) && !Boolean.TryParse(input, out result))
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// 转换int
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ConvertToInt32(string input, int defaultValue)
        {
            int result = defaultValue;
            if (!String.IsNullOrEmpty(input) && !Int32.TryParse(input, out result))
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// 转换long
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ConvertToInt64(string input, long defaultValue)
        {
            long result = defaultValue;
            if (!String.IsNullOrEmpty(input) && !Int64.TryParse(input, out result))
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// 转换DateTime
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(string input, DateTime defaultValue)
        {
            DateTime result = defaultValue;
            if (!String.IsNullOrEmpty(input) && !DateTime.TryParse(input, out result))
            {
                result = defaultValue;
            }
            return result;
        }
        /// <summary>
        /// 转换Decimal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Decimal ConvertToDecimal(string input)
        {
            Decimal result = 0m;
            if (!String.IsNullOrEmpty(input) && !Decimal.TryParse(input, out result))
            {
            }
            return result;
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ConvertToString(string input)
        {
            return string.IsNullOrEmpty(input) ? "" : input;
        }

        /// <summary>
        /// 将字符串转化为long
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static long ConvertToLong(string input, long defaultValue)
        {
            long result = defaultValue;
            if (!String.IsNullOrEmpty(input) && !Int64.TryParse(input, out result))
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// 将其变成 json array 形式的字符串
        /// 直接这么处理的速度比，newstonjson快
        /// 10万， 这个需要0.5秒， newstonJson 需要1秒钟
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string GetStr<T>(IEnumerable<T> arr)
        {
            var val = String.Join(",", arr);
            return $"[{val}]";
        }

        /// <summary>
        /// 时间类型转换为字符串，默认格式为yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <returns></returns>
        public static string ConvertToString(DateTime input, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return input.ToString(format);
        }
        /// <summary>
        /// 时间类型转换为字符串，默认格式为yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <returns></returns>
        public static string ConvertToString(DateTime? input, string format = "yyyy-MM-dd HH:mm:ss")
        {
            if (!input.HasValue) return string.Empty;
            return input.Value.ToString(format);
        }


        /// <summary>
        /// 将传入的实体生成url query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetQuery<T>(this T t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("?");

            var type = t.GetType();

            var newpros = type.GetProperties();

            foreach (var pro in newpros)
            {
                var val = pro.GetValue(t)?.ToString();

                if (!string.IsNullOrEmpty(val))
                {
                    val = System.Web.HttpUtility.UrlEncode(val);
                }

                if (pro == newpros.First())
                    sb.Append($"{pro.Name}={val}");
                else
                    sb.Append($"&{pro.Name}={val}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 字符串转换为double
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ConvertToDouble(string input, double defaultValue)
        {
            double result = defaultValue;
            if (!String.IsNullOrEmpty(input) && !double.TryParse(input, out result))
            {
                result = defaultValue;
            }
            return result;
        }
    }
}