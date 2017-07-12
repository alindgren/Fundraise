using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Owin;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Lifestyles;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartupAttribute(typeof(Fundraise.MvcExample.Startup))]
namespace Fundraise.MvcExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            // 1. Create a new Simple Injector container
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // 2. Configure the container (register)
            // See below for more configuration examples
            container.Register<FundraiseContext>(() =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<FundraiseContext>();
                optionsBuilder.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                return new FundraiseContext(optionsBuilder.Options);
            }, Lifestyle.Scoped);
            container.Register<ICampaignRepository, CampaignRepository>(Lifestyle.Scoped);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            //Registration registration = container.GetRegistration(typeof(ICampaignRepository)).Registration;

            //registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent,
            //    "Reason of suppression");

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            app.Use(async (context, next) => {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    await next();
                }
            });

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MappingConfig.RegisterMaps();

            ConfigureAuth(app);
        }
    }
}
