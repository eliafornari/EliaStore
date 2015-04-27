using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EliaStore.Startup))]
namespace EliaStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
