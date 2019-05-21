using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EM.Management.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {


            return Content("ok");
        }
    }
}
