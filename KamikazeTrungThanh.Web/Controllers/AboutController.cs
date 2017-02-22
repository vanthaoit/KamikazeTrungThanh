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
    public class AboutController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private ICommonService _commonService;
        public AboutController(IProductCategoryService productCategoryService, ICommonService commonService)
        {
            _productCategoryService = productCategoryService;
            _commonService = commonService;
        }
        // GET: About
        public ActionResult Index()
        {
            var alias = "gioi-thieu" ;
            var modelAbout = _productCategoryService.GetByAlias(alias);
            var viewModelAboutResponse = AutoMapper.Mapper.Map<ProductCategory, ProductCategoryViewModel>(modelAbout);
            return View(viewModelAboutResponse);
        }
    }
}