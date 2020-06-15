using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mr_MissSeeks.Startup))]
namespace Mr_MissSeeks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
