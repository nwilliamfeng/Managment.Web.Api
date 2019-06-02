using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management.Web
{
    public static class JsonDataExtension
    {

        public static JsonResultData  SetSuccess (this JsonResultData result)
        {
            result.StatusCode = StatusCodes.SUCCESS;
            return result;
        }

        public static JsonResultData SetFail (this JsonResultData result, string message = null)
        {
            result.StatusCode = StatusCodes.FAIL;
            result.Message = message;
            return result;
        }

        public static JsonResultData ToJsonData<T>(this System.Collections.Generic.IEnumerable<T> lst)
        {
            return new JsonResultData { Count = lst.Count(), Data = lst.ToList(), StatusCode = StatusCodes.SUCCESS };
        }

        public static JsonResultData ToJsonData(this Exception ex)
        {
            return new JsonResultData { Data = null, StatusCode = StatusCodes.ERROR, Message = ex.Message };
        }

    }
}
