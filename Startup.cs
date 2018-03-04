using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(uCommerceMVC.Startup))]
namespace uCommerceMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
