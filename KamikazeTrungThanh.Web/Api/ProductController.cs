using AutoMapper;
using KamikazeTrungThanh.Model.Models;
using KamikazeTrungThanh.Service;
using KamikazeTrungThanh.Web.Infrastructure.Core;
using KamikazeTrungThanh.Web.Infrastructure.Extensions;
using KamikazeTrungThanh.Web.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace KamikazeTrungThanh.Web.Api
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiControllerBase
    {
        #region Initialize

        private IProductService _productService;

        public ProductController(IErrorService errorService, IProductService productService) : base(errorService)
        {
            this._productService = productService;
        }

        #endregion Initialize

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productService.GetAll();

                var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productService.GetById(id);

                var responseData = Mapper.Map<Product, ProductViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel productVm)
        {

            //ProductViewModel productVm = new ProductViewModel()
            //{
            //    ID = 1,
            //    Name = "son nuoc",
            //    Alias = "son-nuoc",
            //    CategoryID = 1,
            //    Price = 1,
            //    Quantity = 1,
            //    Status = true
            //};
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    
                    Product newProduct = new Product();
                    newProduct.UpdateProduct(productVm);
                    newProduct.CreatedDate = DateTime.Now;
                    newProduct.CreatedBy = User.Identity.Name;
                    _productService.Add(newProduct);
                    _productService.Save();

                    var responseData = Mapper.Map<Product, ProductViewModel>(newProduct);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel productVm)
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
                var dbProduct = _productService.GetById(productVm.ID);
                dbProduct.UpdateProduct(productVm);
                dbProduct.UpdatedDate = DateTime.Now;
                dbProduct.UpdatedBy = User.Identity.Name;
                _productService.Update(dbProduct);
                _productService.Save();

                var responseData = Mapper.Map<Product, ProductViewModel>(dbProduct);
                response = request.CreateResponse(HttpStatusCode.Created, responseData);
            }

            return response;
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
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
                    var elseProducts = _productService.Delete(id);
                    _productService.Save();
                    var responseData = Mapper.Map<Product, ProductViewModel>(elseProducts);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("deleteMulti")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, string checkedProducts)
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
                    var listProductID = new JavaScriptSerializer().Deserialize<List<int>>(checkedProducts);
                    foreach (var item in listProductID)
                    {
                        _productService.Delete(item);
                    }
                    _productService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listProductID.Count);
                }
                return response;
            });
        }
    }
}