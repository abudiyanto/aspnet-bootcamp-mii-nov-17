using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BootPress.Startup))]
namespace BootPress
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
