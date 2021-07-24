using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gaming_Club.Startup))]
namespace Gaming_Club
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
