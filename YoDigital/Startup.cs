using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(YoDigital.Startup))]

namespace YoDigital
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
