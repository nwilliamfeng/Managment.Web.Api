using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace EM.Management.Web
{
    public class JsonData
    {
        public int StatusCode { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }

        public int Count { get; set; }

        public static JsonData Success()
        {
            return new JsonData { Data = true, StatusCode = StatusCodes.SUCCESS };
        }

        public static JsonData Fail(string message = null)
        {
            return new JsonData { Data = false, Message = message, StatusCode = StatusCodes.FAIL };
        }

        public static JsonData Error(string message = null)
        {
            return new JsonData { Data = false, Message = message, StatusCode = StatusCodes.ERROR };
        }
    }


    
}