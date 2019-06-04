using System;
using System.Configuration;

namespace CommonUtils
{
    /// <summary>
    /// 配置管理工具类
    /// </summary>
    public class ConfigUtils
    {
        /// <summary>
        /// 从 appsetting 中获取value值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Obsolete("使用方法 GetAppSettingValue 代替")]
        public static string GetConfig(string key)
        {
            var appSetting = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(appSetting))
            {
                throw new Exception($"获取{key}配置失败，请检查配置项！");
            }
            return appSetting;
        }

        /// <summary>
        /// 从appsetting中获取配置值
        /// </summary>
        /// <param name="key">配置名</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        [Obsolete("使用方法 GetAppSettingValue 代替")]
        public static string GetConfig(string key, string defaultValue)
        {
            var val = ConfigurationManager.AppSettings[key];
            return val ?? defaultValue;
        }

        /// <summary>
        /// 从 appsetting 中获取配置值，值为int类型
        /// </summary>
        /// <param name="key">配置名</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        [Obsolete("使用方法 GetAppSettingValue 代替")]
        public static int GetConfig(string key, int defaultValue)
        {
            int result = defaultValue;
            if (ConfigurationManager.AppSettings[key] != null)
            {
                if (!Int32.TryParse(ConfigurationManager.AppSettings[key], out result))
                {
                    result = defaultValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="configName">配置名</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        [Obsolete("使用方法 GetAppSettingValue 代替")]
        public static double GetConfig(string configName, double defaultValue)
        {
            double result = defaultValue;
            if (ConfigurationManager.AppSettings[configName] != null)
            {
                if (!double.TryParse(ConfigurationManager.AppSettings[configName], out result))
                {
                    result = defaultValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="configName">配置名</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        [Obsolete("使用方法 GetAppSettingValue 代替")]
        public static DateTime GetConfig(string configName, DateTime defaultValue)
        {

            DateTime result = defaultValue;
            if (ConfigurationManager.AppSettings[configName] != null)
            {
                if (!DateTime.TryParse(ConfigurationManager.AppSettings[configName], out result))
                {
                    result = defaultValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="configName">配置名</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        [Obsolete("使用方法 GetAppSettingValue 代替")]
        public static bool GetConfig(string configName, bool defaultValue)
        {
            return ConfigurationManager.AppSettings[configName] == null ? defaultValue : ((ConfigurationManager.AppSettings[configName].ToLower() == "true" || ConfigurationManager.AppSettings[configName] == "1") ? true : false);
        }

        /// <summary>
        /// 从 appsetting 中获取value值，返回转换类型后的值
        /// 对于bool值， 配置中 true | 1 都是 true
        /// </summary>
        /// <typeparam name="T">强类型的返回值</typeparam>
        /// <param name="key">appsetting中的Key名称</param>
        /// <returns></returns>       
        public static T GetAppSettingValue<T>(string key)
        {
            var val = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(val))
            {
                throw new Exception($"获取{key}配置失败，请检查配置项！");
            }

            return ConvertValue<T>(val);
        }

        /// <summary>
        /// 从 appsetting 中获取value值,
        /// 若不存在，则从Apollo配置中心获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认返回值</param>
        /// <param name="appID">从Apollo配置中心获取时，此值必填</param>
        /// <param name="sectionName">从Apollo配置中心获取时，此值根据配置来填</param>
        /// <returns></returns>
        public static T GetAppSettingValue<T>(string key, T defaultValue, string appID = "", string sectionName = "ApolloConfig")
        {
            var val = ConfigurationManager.AppSettings[key];
            return val == null ? defaultValue : ConvertValue<T>(val);
        }


        /// <summary>
        /// 从 appsetting 中获取value值，返回转换类型后的值
        /// 对于bool值， 配置中 true | 1 都是 true
        /// </summary>
        /// <typeparam name="T">强类型的返回值</typeparam>
        /// <param name="key">appsetting中的Key名称</param>
        ///   <param name="defaultValue">默认返回值</param>
        /// <returns></returns>
        public static T GetAppSettingValue<T>(string key, T defaultValue)
        {
            var val = ConfigurationManager.AppSettings[key];
            return val == null ? defaultValue : ConvertValue<T>(val);
        }

        /// <summary>
        /// url配置从 appsetting 中获取
        /// </summary>
        /// <param name="key">appsetting中的Key名称</param>
        /// <param name="endChar">结尾字符，默认以'/'结尾</param>
        /// <param name="defaultValue">默认返回值</param>
        /// <param name="appID">从Apollo配置中心获取时，此值必填</param>
        /// <param name="sectionName">从Apollo配置中心获取时，此值根据配置来填</param>
        /// <returns>返回配置中的url</returns>
        public static string GetAppSettingValueForUrl(string key, string defaultValue = "", string endChar = "/")
        {
            var url = GetAppSettingValue<string>(key, defaultValue);
            return AppendEndCharIfNotExists(url, endChar);
        }

        /// <summary>
        /// 如果配置中的值不含有/，那么增加斜杠/到尾部
        /// </summary>
        /// <param name="value"></param>
        /// <param name="endChar">结束字符串</param>
        /// <returns></returns>
        public static string AppendEndCharIfNotExists(string value, string endChar = "/")
        {
            return value.EndsWith(endChar) ? value : value + endChar;
        }


        /// <summary>
        /// 将配置中的值转换成对应的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        private static T ConvertValue<T>(string val)
        {
            // 从原来的代码看， bool类型的配置，里面可能是 true，也可能是“1”， 这里需要特殊判断一下
            if (typeof(T) == typeof(bool))
            {
                var lv = val.ToLower();
                val = lv == "1" && lv == "true" ? "true" : "false";
            }

            return (T)Convert.ChangeType(val, typeof(T));
        }
    }
}
