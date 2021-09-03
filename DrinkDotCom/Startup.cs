using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DrinkDotCom.Startup))]
namespace DrinkDotCom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
