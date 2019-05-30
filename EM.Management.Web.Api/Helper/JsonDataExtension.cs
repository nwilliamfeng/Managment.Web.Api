using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management.Web
{
    public static class JsonDataExtension
    {

        public static JsonData  SetSuccess (this JsonData result)
        {
            result.StatusCode = StatusCodes.SUCCESS;
            return result;
        }

        public static JsonData SetFail (this JsonData result, string message = null)
        {
            result.StatusCode = StatusCodes.FAIL;
            result.Message = message;
            return result;
        }

        public static JsonData ToJsonData<T>(this System.Collections.Generic.IEnumerable<T> lst)
        {
            return new JsonData { Count = lst.Count(), Data = lst.ToList(), StatusCode = StatusCodes.SUCCESS };
        }

        public static JsonData ToJsonData(this Exception ex)
        {
            return new JsonData { Data = null, StatusCode = StatusCodes.ERROR, Message = ex.Message };
        }

    }
}
