using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KamikazeTrungThanh.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Navigation()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult Banner()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult BoostrapPopUp()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Bottom()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult Latest()
        {
            return PartialView();
        }
    }
}