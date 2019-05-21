using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace EM.Management.API.Controllers
{
    public class LiquidationController : ApiController
    {
        //public static JsonResult<List<MonthlyReportEntity>> GetMonthReport(string clearDate, int platformid = -1, int pageIndex = 1)
        //{
        //    return LiquidationData.GetMonthReport(clearDate, platformid, pageIndex);


        //}

        //public  JsonResult<List<DayReportEntity>> GetDayReport(string clearDate, int platformid, int pageIndex = 1)
        //{
        //    return LiquidationData.GetDayReport(clearDate, platformid, pageIndex);
        //}

        //public static JsonResult<List<ClearpointflowEntity>> GetErrorClearPointFlow(string clearDate, int platformid, int pageIndex = 1)
        //{
        //    return LiquidationData.GetErrorClearPointFlow(clearDate, platformid, pageIndex);
        //}

        //public static JsonResult<List<ClearConfig>> GetClearHistory(string clearDate, int platformid, int pageIndex = 0)
        //{
        //    if (!string.IsNullOrEmpty(clearDate))
        //    {
        //        var strs = clearDate.Split('-');
        //        if (new DateTime(int.Parse(strs[0]), int.Parse(strs[1]), int.Parse(strs[2])) >= DateTime.Now.Date)
        //        {
        //            return new JsonResult<List<ClearConfig>> { result = 1, };
        //        }
        //    }
        //    return LiquidationData.GetClearHistory(clearDate, platformid, pageIndex);
        //}


        //public static JsonResult<List<ClearUserPointEntity>> GetLastClearUsers(string userID, string clearDate, int platformid, int pageIndex = 0)
        //{

        //    return LiquidationData.GetLastClearUsers(userID, clearDate, platformid, pageIndex);
        //}
    }
}
