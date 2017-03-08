using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Tutorial_2017.Startup))]
namespace MVC_Tutorial_2017
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
