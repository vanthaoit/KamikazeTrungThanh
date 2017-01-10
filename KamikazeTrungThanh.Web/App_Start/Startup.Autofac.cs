using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using KamikazeTrungThanh.Data;
using KamikazeTrungThanh.Data.Infrastructure;
using KamikazeTrungThanh.Data.Repositories;
using KamikazeTrungThanh.Model.Models;
using KamikazeTrungThanh.Service;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(KamikazeTrungThanh.Web.App_Start.Startup))]

namespace KamikazeTrungThanh.Web.App_Start
{
    public partial class Startup
    {
        public void ConfigureAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // register controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // register infrastructure
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            // register repositories
            builder.RegisterAssemblyTypes(typeof(ProductRepository).Assembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            // register Service

            builder.RegisterAssemblyTypes(typeof(ProductService).Assembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerRequest();
            // register context
            builder.RegisterType<KamikazeTrungThanhDbContext>().AsSelf().InstancePerRequest();

            //Asp.net Identity

            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();

            IContainer container = builder.Build();
            // override cơ chế mặc định bằng register
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver =
                new AutofacWebApiDependencyResolver(container); // set the WebApi DependencyResolver
        }
    }
}