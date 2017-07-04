using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Fundraise.MvcExample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //// 1. Create a new Simple Injector container
            //var container = new Container();

            //// 2. Configure the container (register)
            //// See below for more configuration examples
            //container.Register<FundraiseContext>(() =>
            //{
            //    var optionsBuilder = new DbContextOptionsBuilder<FundraiseContext>();
            //    optionsBuilder.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            //    return new FundraiseContext(optionsBuilder.Options);
            //});
            //container.Register<ICampaignRepository, CampaignRepository>(Lifestyle.Singleton);

            //// 3. Optionally verify the container's configuration.
            //container.Verify();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
