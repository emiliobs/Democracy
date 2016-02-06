using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Democracy.Startup))]
namespace Democracy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
