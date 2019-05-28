using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.WebApi;

namespace EM.Management.Web
{
    public static class AutofacExtension
    {
        //public static IContainer RegistComponentsWithSpecifiedSuffix(this ContainerBuilder builder, params string[] suffixs)
        //{

        //    builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

        //    var amblys = Directory.GetFiles(HttpRuntime.AppDomainAppPath + "bin")
        //         .Where(x =>Path.GetFileName(x).Contains("EM.Management") && x.EndsWith(".dll"))
        //         .ToList();

        //       amblys .Select(x => Assembly.LoadFile(x))
        //        .ToList()
        //       .ForEach(x =>
        //       {
        //           suffixs.ToList().ForEach(suffix =>
        //           {
        //               if (x.GetTypes().Any(c => c.IsClass && !c.IsAbstract && c.Name.EndsWith(suffix)))
        //                   builder.RegisterAssemblyTypes(x).Where(t => t.Name.EndsWith(suffix)).AsImplementedInterfaces();
        //           });

        //       });

        //    return builder.Build();
        //}

        public static IContainer RegistComponentsWithSpecifiedSuffix(this ContainerBuilder builder, params string[] suffixs)
        {

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var amblys = Directory.GetFiles(HttpRuntime.AppDomainAppPath + "bin")
                 .Where(x => Path.GetFileName(x).Contains("EM.Management") && x.EndsWith(".dll"))
                 .Select(x => Assembly.LoadFile(x))
             .Where(x => suffixs.Any(suffix => x.GetTypes().Any(c => c.IsClass && !c.IsAbstract && c.Name.EndsWith(suffix))))
             .ToArray();


            builder.RegisterAssemblyTypes(amblys).AsImplementedInterfaces();



            return builder.Build();
        }
    }
}