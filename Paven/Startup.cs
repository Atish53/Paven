using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Paven.Startup))]
namespace Paven
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
