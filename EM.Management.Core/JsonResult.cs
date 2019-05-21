using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management
{
    public sealed class JsonResult<T>
    {
        public T Data { get; set; }

        public int StatusCode { get; set; }

        public int Count { get; set; }

        public string Message { get; set; }
    }
}
