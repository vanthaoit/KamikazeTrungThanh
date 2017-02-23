using AutoMapper;
using KamikazeTrungThanh.Model.Models;
using KamikazeTrungThanh.Service;
using KamikazeTrungThanh.Web.Infrastructure.Core;
using KamikazeTrungThanh.Web.Infrastructure.Extensions;
using KamikazeTrungThanh.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace KamikazeTrungThanh.Web.Api
{
    [RoutePrefix("api/slide")]
    [Authorize]
    public class SlideController : ApiControllerBase
    {
        #region Initialize

        private ISlideService _slideService;

        public SlideController(IErrorService errorService, ISlideService slideService) : base(errorService)
        {
            this._slideService = slideService;
        }

        #endregion Initialize

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _slideService.GetAll();
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _slideService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(query);

                var paginationSet = new paginationSet<SlideViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
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
                    var dbSlideGetById = _slideService.GetById(id);
                    var responseData = Mapper.Map<Slide, SlideViewModel>(dbSlideGetById);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, SlideViewModel slideVm)
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
                    Slide newSlide = new Slide();
                    newSlide.UpdateSlide(slideVm);
                    _slideService.Add(newSlide);
                    _slideService.Save();

                    var responseData = Mapper.Map<Slide, SlideViewModel>(newSlide);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, SlideViewModel slideVm)
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
                    var dbUpdateSlide = _slideService.GetById(slideVm.ID);
                    dbUpdateSlide.UpdateSlide(slideVm);
                    _slideService.Update(dbUpdateSlide);
                    _slideService.Save();
                    var responseData = Mapper.Map<Slide, SlideViewModel>(dbUpdateSlide);
                    response = request.CreateResponse(HttpStatusCode.Created, dbUpdateSlide);
                }

                return response;
            });
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
                    var deleteSlide = _slideService.Delete(id);
                    _slideService.Save();
                    var responseData = Mapper.Map<Slide, SlideViewModel>(deleteSlide);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deleteMulti")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, string checkSlides)
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
                    var listSlideId = new JavaScriptSerializer().Deserialize<List<int>>(checkSlides);
                    foreach (var item in listSlideId)
                    {
                        _slideService.Delete(item);
                    }
                    _slideService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, listSlideId.Count);
                }

                return response;
            });
        }
    }
}