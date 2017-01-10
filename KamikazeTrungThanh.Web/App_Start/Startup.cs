using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(KamikazeTrungThanh.Web.App_Start.Startup))]

namespace KamikazeTrungThanh.Web.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAutofac(app);
            ConfigureAuth(app);
        }
    }
}