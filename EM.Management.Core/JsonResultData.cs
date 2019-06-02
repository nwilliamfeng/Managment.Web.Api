using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace EM.Management
{
    public class JsonResultData<T>
    {
        public int StatusCode { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }

        public int Count { get; set; }

     
    }


    
}