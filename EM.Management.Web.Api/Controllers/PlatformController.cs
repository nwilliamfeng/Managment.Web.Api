using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EM.Management.Service;
using WebApi.OutputCache.V2;

namespace EM.Management.Web.Controllers
{
    [Authentication]
    public class PlatformController : ApiController
    {
        private IPlatformService _platformService;     

        public PlatformController(IPlatformService platformService)
        {
            this._platformService = platformService;
        }

        [CacheOutput(ServerTimeSpan =10)]
        [HttpGet]
        public async Task<IHttpActionResult>  Platforms()
        {
            var lst = await this._platformService.GetPlatforms();        
            return this.JsonResult(lst.ToJsonResultData());
        }

 
    }
}
