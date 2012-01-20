using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doberman.Example.Mvc3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //throw (new Exception("Test!"));

            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
