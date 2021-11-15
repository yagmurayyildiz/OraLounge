using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web.OraLounge.Startup))]
namespace Web.OraLounge
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}