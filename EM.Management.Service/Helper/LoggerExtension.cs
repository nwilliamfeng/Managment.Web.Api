using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management.Service
{
    public static class LoggerExtension
    {
        public static void Log(this object obj)
        {
           LogWrapper.LogInfo("service_info",obj);
        }
    }
}
