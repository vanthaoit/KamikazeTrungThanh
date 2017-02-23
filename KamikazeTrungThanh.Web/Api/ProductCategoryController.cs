using KamikazeTrungThanh.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KamikazeTrungThanh.Service;
using AutoMapper;
using KamikazeTrungThanh.Model.Models;
using KamikazeTrungThanh.Web.Models;
using KamikazeTrungThanh.Web.Infrastructure.Extensions;
using System.Web.Script.Serialization;

namespace KamikazeTrungThanh.Web.Api
{
    [RoutePrefix("api/productcategory")]
    [Authorize]
    public class ProductCategoryController : ApiControllerBase
    {
        #region Initialize
        private IProductCategoryService _productCategoryService;
        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
        {
            _productCategoryService = productCategoryService;
        }
        #endregion Initialize
          
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize=20)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _productCategoryService.GetAll(keyword);
                var totalRow = 0;
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var productCategoryVm = Mapper.Map<IEnumerable<ProductCategory>,IEnumerable<ProductCategoryViewModel>>(query);
                var paginationSet = new paginationSet<ProductCategory>()
                {
                    Page = page,
                    Items = query,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal) totalRow / pageSize)
                };
                response = request.CreateResponse(HttpStatusCode.OK,paginationSet);
                return response;
            });

        }


        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _productCategoryService.GetAll();
                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK,responseData);
                return response;

            });
           
            
        }
        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                var model = _productCategoryService.GetById(id);
                var responseViewModelData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(model);
                response = request.CreateResponse(HttpStatusCode.OK,responseViewModelData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request,()=> {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    ProductCategory newProductCategory = new ProductCategory();
                    newProductCategory.UpdateProductCategory(productCategoryVm);
                    newProductCategory.CreatedDate = DateTime.Now;
                    newProductCategory.CreatedBy = User.Identity.Name;
                    _productCategoryService.Add(newProductCategory);
                    _productCategoryService.Save();
                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created,responseData);
                }
                return response;
            });
           
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            //ProductViewModel productVm = new ProductViewModel()
            //{
            //    ID = 2,
            //    Name = "son nuoc",
            //    Alias = "son-nuoc",
            //    CategoryID = 1,
            //    Price = 1,
            //    Quantity = 1,
            //    Status = true
            //};
            HttpResponseMessage response = null;
            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                var dbProductCategory = _productCategoryService.GetById(productCategoryVm.ID);
                dbProductCategory.UpdateProductCategory(productCategoryVm);
                dbProductCategory.UpdatedDate = DateTime.Now;
                dbProductCategory.UpdatedBy = User.Identity.Name;
                _productCategoryService.Update(dbProductCategory);
                _productCategoryService.Save();

                var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(dbProductCategory);
                response = request.CreateResponse(HttpStatusCode.Created, responseData);
            }

            return response;
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request,int id)
        {
            return CreateHttpResponse(request,()=> {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,ModelState.IsValid);
                }
                else
                {
                    var deleteProductCategory = _productCategoryService.Delete(id);
                    _productCategoryService.Save();
                    var responseProductCategory = Mapper.Map<ProductCategory, ProductCategoryViewModel>(deleteProductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created,responseProductCategory);
                }
                return response;

            });
        }

        [Route("deleteMulti")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, string checkedProductCategories)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listProductCategoryID = new JavaScriptSerializer().Deserialize<List<int>>(checkedProductCategories);
                    foreach (var item in listProductCategoryID)
                    {
                        _productCategoryService.Delete(item);
                    }
                    _productCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listProductCategoryID.Count);
                }
                return response;
            });
        }
    }
}