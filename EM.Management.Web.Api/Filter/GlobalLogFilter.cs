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

        public GlobalLogFilter()
        {
            if (log == null)
            {
                log4net.Config.XmlConfigurator.Configure();
                log = log4net.LogManager.GetLogger("api_info");
            }
        }

        public override  Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            string result = null;
            if(actionExecutedContext.Response!=null)
                result = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;
            var content = $"after call url:{actionExecutedContext.Request.RequestUri},params:{JsonConvert.SerializeObject(actionExecutedContext.ActionContext.ActionArguments)} and the return result is : {result}";
            log.Info(content);
            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }

        //public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        //{
            
        //    var content = $"call url:{actionContext.Request.RequestUri},params:{JsonConvert.SerializeObject(actionContext.ActionArguments)}";

        //    log.Info(content);
          
        //    return base.OnActionExecutingAsync(actionContext, cancellationToken);
        //}

        
    }
}