using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyCompany.Startup))]
namespace MyCompany
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
