using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KamikazeTrungThanh.Web.Controllers
{
    public class PortfolioController : Controller
    {
        // GET: Portfolio
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult ProductCategoryPartialView()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult ProductHeaderPartialView()
        {
            return PartialView();
        }

    }
}