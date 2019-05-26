using Autofac;
using Autofac.Integration.WebApi;
using EM.Management.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.IO;

namespace EM.Management.Web
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
          
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
           
           var amblys= Directory.GetFiles(HttpRuntime.AppDomainAppPath+"bin")
                .Where(x=>x.Contains("EM.Management") &&  x.EndsWith(".dll"))
                .ToList()
                .Select(x=>Assembly.LoadFile(x));
            amblys.ToList()
                .ForEach(x =>
                {
                    if (x.GetTypes().Any(c => c.IsClass && !c.IsAbstract && c.Name.EndsWith("Service")))
                        builder.RegisterAssemblyTypes(x).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
                });

       //     builder.RegisterAssemblyTypes(typeof(PlatformService).Assembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();

            //builder.RegisterType<DbFactory>()
            //       .As<IDbFactory>()
            //       .InstancePerRequest();

            //builder.RegisterGeneric(typeof(GenericRepository<>))
            //       .As(typeof(IGenericRepository<>))
            //       .InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }

        public static void Run()
        {
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
        }

    }
}