using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Lifestyles;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Configuration;
using MediatR;
using System.Collections.Generic;
using Fundraise.MvcExample.Requests;
using System.Linq;

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
            container.Register<FundraiseContext>(() =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<FundraiseContext>();
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                return new FundraiseContext(optionsBuilder.Options);
            }, Lifestyle.Scoped);

            container.Register<ICampaignRepository, CampaignRepository>(Lifestyle.Scoped);
            container.Register<IDonationRepository, DonationRepository>(Lifestyle.Scoped);
            container.RegisterSingleton<IMediator, Mediator>();
            var assemblies = GetAssemblies().ToArray();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IRequestHandler<>), assemblies);

            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton(new MultiInstanceFactory(container.GetAllInstances));

            var identityDbContext = Models.ApplicationDbContext.Create();
            container.Register<IUserStore<Models.ApplicationUser>>(() => new Microsoft.AspNet.Identity.EntityFramework.UserStore<Models.ApplicationUser>(identityDbContext));

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

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
            container.Register<IAuthenticationManager>(() => HttpContext.Current.GetOwinContext().Authentication);

            Stripe.StripeConfiguration.SetApiKey(ConfigurationManager.AppSettings["StripeSecretKey"]);
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IMediator).GetTypeInfo().Assembly;
            yield return typeof(Donate).GetTypeInfo().Assembly;
        }
    }
}
