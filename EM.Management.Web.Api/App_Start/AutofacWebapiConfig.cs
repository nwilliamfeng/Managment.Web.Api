using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using log4net;

namespace EM.Management.Web
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container { get; private set; }

        private static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));  
        }


        private static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
           
            Container=  builder.RegistComponentsWithSpecifiedSuffix("Repository","Cache","Service");
            return Container;
        }

        public static void Run()
        {
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
        }

    }
}