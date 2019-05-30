using log4net;
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

        public GlobalExceptionFilter()
        {
            if (log == null)
            {
                log4net.Config.XmlConfigurator.Configure();
                log = log4net.LogManager.GetLogger("api_error");
            }
        }
 
        public override async Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var content = $"when call url:{actionExecutedContext.Request.RequestUri},params:{JsonConvert.SerializeObject(actionExecutedContext.ActionContext.ActionArguments)} , raise an exception: ";

            log.Error(content, actionExecutedContext.Exception);
            var actionResult = actionExecutedContext.Request.JsonResult(actionExecutedContext.Exception.ToJsonData());
            var msg =await  actionResult .ExecuteAsync(cancellationToken);
            msg.StatusCode = HttpStatusCode.InternalServerError;
            actionExecutedContext.Response = msg;
            // return base.OnExceptionAsync(actionExecutedContext, cancellationToken);
        }
    }
}