using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace EM.Management.Web
{
    public static class JsonResultExtension
    {
        private static JsonSerializerSettings GetSettings()
        {
            return new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
        }

        public static IHttpActionResult JsonResult<T>(this ApiController controller,T data)
        {
             
            return new JsonResult<T>(data, GetSettings(), System.Text.Encoding.UTF8, controller);
       
        }

        public static IHttpActionResult JsonResult<T>(this HttpRequestMessage httpRequestMsg, T data)
        {
           
            return new JsonResult<T>(data, GetSettings(), System.Text.Encoding.UTF8, httpRequestMsg);

        }
    }
}