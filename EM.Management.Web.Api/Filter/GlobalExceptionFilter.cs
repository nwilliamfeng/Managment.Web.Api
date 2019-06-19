using log4net;
using Microcomm.Web.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace EM.Management.Web.Filter
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private static ILog log ;
        private static Func<HttpActionExecutedContext, string> loggerHandle;

        public GlobalExceptionFilter()
        {
            if (log == null)
            {
                log4net.Config.XmlConfigurator.Configure();
                log = log4net.LogManager.GetLogger("api_error");
            }
            if (loggerHandle == null)
            {
                loggerHandle = actionExecutedContext =>
                {
                   return  $"when call url:{actionExecutedContext.Request.RequestUri},params:{JsonConvert.SerializeObject(actionExecutedContext.ActionContext.ActionArguments)} , raise an exception: ";

                };
            }
        }
 
        public override async Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            
            log.Error(this.LoggerHandle(actionExecutedContext), actionExecutedContext.Exception);
            var actionResult = actionExecutedContext.Request.JsonResult(actionExecutedContext.Exception.ToJson());
            var msg =await  actionResult .ExecuteAsync(cancellationToken);
            msg.StatusCode = HttpStatusCode.InternalServerError;
            actionExecutedContext.Response = msg;
            await base.OnExceptionAsync(actionExecutedContext, cancellationToken);
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