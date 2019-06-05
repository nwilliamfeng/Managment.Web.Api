using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using WebApi.OutputCache.V2;

namespace EM.Management.Web
{
    public class CacheOutputWithPostAttribute: CacheOutputAttribute
    {
        private static Dictionary<string, List<object>> paramDic = new Dictionary<string, List<object>>();

        protected override bool IsCachingAllowed(HttpActionContext actionContext, bool anonymousOnly)
        {
            var action = actionContext.Request.RequestUri.AbsolutePath;
            if (!paramDic.ContainsKey(action))
            {
                paramDic[action] = actionContext.ActionArguments.Select(x => x.Value).ToList();
                return false;
            }
            else
            {
                var oldParas = paramDic[action];
                if (oldParas == null || oldParas.Count == 0)
                    return true;
                var currParas = actionContext.ActionArguments.Select(x => x.Value).ToList();
                if (oldParas.Count != currParas.Count)
                {
                    paramDic[action] = currParas;
                    return false;
                }
                if(!oldParas.Any(x => !currParas.Contains(x)))
                {
                    paramDic[action] = currParas;
                    return false;
                }
                return base.IsCachingAllowed(actionContext,anonymousOnly);   
            }

        }
    }
}