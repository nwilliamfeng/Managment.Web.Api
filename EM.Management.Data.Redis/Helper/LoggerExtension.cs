using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management.Data.Redis
{
    internal static class LoggerExtension
    {
        public static void Log(this object obj)
        {
           LogWrapper.LogInfo("redis_info",obj);
        }
    }
}
