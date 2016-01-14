﻿using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Cors;
using Microsoft.Owin.Security.OAuth;
using WebApiExtAuth.Api.Data;

namespace WebApiExtAuth.Api.Authorization
{
  public class ApplicationOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
  {
    public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    {
      // This call is required...
      // but we're not using client authentication, so validate and move on...
      await Task.FromResult(context.Validated());
    }

    /// <summary>
    /// Vdalidate credential and generate a token
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    {
      // This statement allows Cors for the token middleware provider only
      // Still need to enable Cors for webapi installing package and calling 
      // app.UseCors(...)
      // IE may not set Origin in certain circumstances
      context.OwinContext.Response.Headers.Add(CorsConstants.AccessControlAllowOrigin, new[] {
        "*"
      });

      var user = (new InMemoryAuthRepository()).Find(context.UserName, context.Password);
      if (user == null)
      {
        context.SetError("invalid_grant", "The user name or password is incorrect.");
      }
      else
      {
        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
        identity.AddClaim(new Claim(ClaimTypes.Role, "user"));

        context.Validated(identity);
      }
    }
  }
}