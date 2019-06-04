using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using EM.Management.Service;
using WebApi.OutputCache.V2;

namespace EM.Management.Web.Controllers
{
  
    public class AuthController : ApiController
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [CacheOutput]
        [HttpPost]      
        public async Task<IHttpActionResult> Login(string userId,string password )
        {
            try
            {
                var result = await this._authService.Login(userId,password);
                return this.JsonResult(result); 
            }
            catch(Exception ex)
            {
                return this.JsonResult(ex.Message.ToJsonWithError());
            }
            
        }


    }
}