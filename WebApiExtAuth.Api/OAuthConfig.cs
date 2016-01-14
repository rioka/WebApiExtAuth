using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using WebApiExtAuth.Api.Authorization;

namespace WebApiExtAuth.Api
{
  public class OAuthConfig
  {
    internal static void Configure(IAppBuilder app)
    {
      var oAuthOptions = new OAuthAuthorizationServerOptions {
        TokenEndpointPath = new PathString("/Token"),
        Provider = new ApplicationOAuthAuthorizationServerProvider(),
        AccessTokenExpireTimeSpan = TimeSpan.FromDays(7),
        AllowInsecureHttp = true
      };

      // Authorize users via username/password
      app.UseOAuthAuthorizationServer(oAuthOptions);

      // Authorize users via username/password
      app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
    }
  }
}