using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(beeftechee.Startup))]
namespace beeftechee
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
