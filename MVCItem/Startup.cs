using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCItem.Startup))]
namespace MVCItem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
