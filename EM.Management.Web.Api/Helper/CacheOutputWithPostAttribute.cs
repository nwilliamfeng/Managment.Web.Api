using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebApi.OutputCache.V2;

namespace EM.Management.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheOutputWithPostAttribute : CacheOutputAttribute
    {
        private static Dictionary<string, List<object>> paramDic = new Dictionary<string, List<object>>();
        private bool _result = false;

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var action = actionContext.Request.RequestUri.AbsolutePath;
            if (!paramDic.ContainsKey(action))
            {
                paramDic.Add(action, actionContext.ActionArguments.Select(x => x.Value).ToList());
               _result=false;
            }
            else
            {
                var oldParas = paramDic[action];

                var currParas = actionContext.ActionArguments.Select(x => x.Value).ToList();
                if (oldParas.Count != currParas.Count)
                {
                    paramDic[action] = currParas;
                    _result= false;
                }
                if (oldParas.Any(x => !currParas.Contains(x)))
                {
                    paramDic[action] = currParas;
                    _result= false;
                }
                else
                    _result= true;
            }
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
          
        }

       

        protected override bool IsCachingAllowed(HttpActionContext actionContext, bool anonymousOnly)
        {
            return this._result;     //这里不能进行执行判断，而是要将判断逻辑移到OnActionExecutingAsync 上，因为IsCachingAllowed会被调用2次，因为返回的结果不一样会导致base.OnActionExecutingAsync报错
        }

    }
}