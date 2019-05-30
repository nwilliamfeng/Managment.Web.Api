using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management
{
    public static class LogWrapper
    {


        static LogWrapper()
        {

            log4net.Config.XmlConfigurator.Configure();
        }

        public static void LogInfo(string logName, object info)
        {
            var log = log4net.LogManager.GetLogger(logName);
            log.Info(Newtonsoft.Json.JsonConvert.SerializeObject( info));
        }

     

        public static void LogError(string logName, Exception ex)
        {
            var log = log4net.LogManager.GetLogger(logName);
            log.Error(ex);
        }
    }
}
