using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RejestrOsobZaginionych.Startup))]
namespace RejestrOsobZaginionych
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
