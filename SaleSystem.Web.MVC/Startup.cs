using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SaleSystem.Web.MVC.Startup))]
namespace SaleSystem.Web.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
