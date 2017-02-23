using KamikazeTrungThanh.Service;
using KamikazeTrungThanh.Web.Infrastructure.Core;
using System.Web.Http;

namespace KamikazeTrungThanh.Web.Api
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiControllerBase
    {
        public HomeController(IErrorService errorService) : base(errorService)
        {
        }

        [HttpGet]
        [Route("MethodDefault")]
        public string MethodDefault()
        {
            return "Hello, Administrator";
        }
    }
}