using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DarrylSite.Startup))]
namespace DarrylSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
