using KamikazeTrungThanh.Model.Models;
using KamikazeTrungThanh.Service;
using KamikazeTrungThanh.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace KamikazeTrungThanh.Web.Controllers
{
    public class PortfolioController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private IProductService _productService;
        private ICommonService _commonService;
        public string aliasDefault = "mai-xep-di-dong";
        public int idDefault = 0;
        public string DefaultOrIndex = "default";
        public PortfolioController(IProductCategoryService productCategoryService, IProductService productService,ICommonService commonService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
            _commonService = commonService;
        }

        public ActionResult Layouts(string alias, int id)
        {
            aliasDefault = alias;
            idDefault = id;

            ViewBag.aliasDefault = aliasDefault;
            ViewBag.idDefault = idDefault;

            
            
            return View();
        }

        [ChildActionOnly]
        public ActionResult Default(string alias)
        {
            if (!string.IsNullOrEmpty(alias)) aliasDefault = alias;
            var modelData = _productService.GetAllByAlias(aliasDefault);
            var viewModelData = AutoMapper.Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(modelData);

            return PartialView(viewModelData);
        }
        // GET: Portfolio
        
        [HttpPost]
        public ActionResult Index(string aliasProductCategory)
        {
            if(!string.IsNullOrEmpty(aliasProductCategory))
            {
                aliasDefault = aliasProductCategory;
                //aliasDefault = Request["aliasProductCategory"].ToString();
            }
            
            var modelData = _productService.GetAllByAlias(aliasDefault);
            var viewModelData = AutoMapper.Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(modelData);
            ViewBag.aliasDefault =aliasDefault ;
            return PartialView(viewModelData);
        }
        
        [HttpPost]
        public ActionResult Detail(string alias,string aliasCategory, int id)
        {
            ViewBag.aliasDefault = aliasDefault;
            ViewBag.aliasCategory = aliasCategory;
            var productModel = _productService.GetById(id);
            var viewModelData = AutoMapper.Mapper.Map<Product,ProductViewModel>(productModel);
            List<string> moreImages = new JavaScriptSerializer().Deserialize<List<string>>(viewModelData.MoreImages);
            ViewBag.moreImages = moreImages;
            return View(viewModelData);
        }
        [ChildActionOnly]
        public ActionResult ProductCategoryPartialView(string ikeyword)
        {
            string keyword = ikeyword ;
            var modelData = _commonService.GetNavigationMenuChild(keyword);
            var viewModelData = AutoMapper.Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(modelData);
            return PartialView(viewModelData);
        }

        [ChildActionOnly]
        public ActionResult ProductHeaderPartialView(string alias)
        {
            aliasDefault = alias;
            var productCategoryBreadcrumb = _productCategoryService.GetByAlias(aliasDefault);
            var listBreadcrumb = _commonService.GetBreadcrumb(productCategoryBreadcrumb.ID);
            ViewBag.listBreadcrumb = AutoMapper.Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(listBreadcrumb);
            return PartialView();
        }

    }
}