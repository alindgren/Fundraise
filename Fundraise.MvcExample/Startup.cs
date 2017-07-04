using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fundraise.MvcExample.Startup))]
namespace Fundraise.MvcExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
