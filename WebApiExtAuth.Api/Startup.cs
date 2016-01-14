using System.Web.Http;
using Microsoft.Owin;
using Owin;
using WebApiExtAuth.Api;

[assembly: OwinStartup(typeof(Startup))]

namespace WebApiExtAuth.Api
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      var config = new HttpConfiguration();
      WebApiConfig.Register(config);
      OAuthConfig.Configure(app);

      app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
      app.UseWebApi(config);
    }
  }
}