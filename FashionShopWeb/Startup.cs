using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(FashionShopWeb.Startup))]

namespace FashionShopWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseCookieAuthentication(new CookieAuthenticationOptions()
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Home/Index"),             
            //});
        }
    }
}
