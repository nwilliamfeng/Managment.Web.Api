using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
 

namespace EM.Management.Web.Controllers
{
  
    public class AuthController : ApiController
    {
     

        [HttpPost]      
        public IHttpActionResult Login(string userId,string password )
        {
            return this.JsonResult((userId + password).ToJson());
        }


    }
}