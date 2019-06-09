using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using EM.Management.Service;
using Newtonsoft.Json.Linq;
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

     
       
     
        [HttpPost]
        [Filter.JObjectParamValidate(Params ="userId,password")]
        public async Task<IHttpActionResult> Login([FromBody]JObject param )
        {
            try
            {
                var result = await this._authService.Login(param["userId"].Value<string>()  ,param["password"].Value<string>());
                return this.JsonResult(result); 
            }
            catch(Exception ex)
            {
                return this.JsonResult(ex.Message.ToJsonWithError());
            }
            
        }

       
        [HttpPost]
        [Authentication]
        public string Logout(string userId,string accessToken)
        {
            return "";
        }


    }
}