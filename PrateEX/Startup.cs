using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PrateEX.Startup))]
namespace PrateEX
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
