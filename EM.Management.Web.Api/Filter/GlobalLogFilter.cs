using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Newtonsoft.Json;

namespace EM.Management.Web.Filter
{
    public class GlobalLogFilter : ActionFilterAttribute
    {
        private static ILog log ;
        private static Func<HttpActionExecutedContext, string> loggerHandle;

        public GlobalLogFilter()
        {
            if (log == null) 
            {
                log4net.Config.XmlConfigurator.Configure();
                log= log4net.LogManager.GetLogger("api_info");
            }
            if (loggerHandle == null)
            {
                loggerHandle = actionExecutedContext =>
                {
                    string result = null;
                    if (actionExecutedContext.Response != null)
                        result = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;
                    return $"after call url:{actionExecutedContext.Request.RequestUri},params:{JsonConvert.SerializeObject(actionExecutedContext.ActionContext.ActionArguments)} and the return result is : {result}";
                };
            }
        }

        public override  Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            this.Logger.Info(LoggerHandle(actionExecutedContext));
            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }

        public Func<HttpActionExecutedContext, string> LoggerHandle
        {
            get { return loggerHandle; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                loggerHandle = value;
            }
        }

        public ILog Logger
        {
            get { return log; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                log = value;
            }
        }

        
    }
}