using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BexMediaCom.Startup))]
namespace BexMediaCom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
