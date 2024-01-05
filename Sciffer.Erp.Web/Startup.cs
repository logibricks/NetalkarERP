using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sciffer.Erp.Web.Startup))]
namespace Sciffer.Erp.Web
{

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
