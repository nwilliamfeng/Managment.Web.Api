using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


using System.Web.Routing;

namespace EM.Management.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
             EM.Management.Data.Redis.RedisCache.ConfigName = "RedisServer";
         
            GlobalConfiguration.Configure(WebApiConfig.Register);
        
          
        }
    }
}
