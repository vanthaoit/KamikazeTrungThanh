using KamikazeTrungThanh.Model.Models;
using KamikazeTrungThanh.Service;
using KamikazeTrungThanh.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KamikazeTrungThanh.Web.Controllers
{
    public class HomeController : Controller
    {
        private ISlideService _slideService;
        private ICommonService _commonService;
        private IProductCategoryService _productCategoryService;

        public HomeController(ISlideService slideService,ICommonService commonService, IProductCategoryService productCategoryService)
        {
            _slideService = slideService;
            _commonService = commonService;
            _productCategoryService = productCategoryService;
        }
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
            var slideModel = _slideService.GetAll();
            var slideViewModel = AutoMapper.Mapper.Map<IEnumerable<Slide>,IEnumerable<SlideViewModel>>(slideModel);
            return PartialView(slideViewModel);
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