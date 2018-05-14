using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ari_ReportProject.Startup))]
namespace Ari_ReportProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
