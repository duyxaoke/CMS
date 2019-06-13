using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMS.Presentation.Startup))]
namespace CMS.Presentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
