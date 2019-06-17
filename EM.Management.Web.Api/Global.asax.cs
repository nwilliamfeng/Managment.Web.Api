using Microcomm.Web.Http.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


using System.Web.Routing;

namespace EM.Management.Web
{
    

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
             EM.Management.Data.Redis.RedisCache.ConfigName = "RedisServer";

            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration, "EM.Management",new string[] { "Repository", "Cache", "Service" });
         
            GlobalConfiguration.Configure(WebApiConfig.Register);
        
           
          
        }
    }
}
