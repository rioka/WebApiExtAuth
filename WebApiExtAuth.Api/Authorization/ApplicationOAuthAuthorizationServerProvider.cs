using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

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

    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    {
      if (context.UserName != context.Password)
      {
        context.SetError("invalid_grant", "The user name or password is incorrect.");
      }
      else
      {
        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
        identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
        identity.AddClaim(new Claim("sub", context.UserName));

        context.Validated(identity);
      }
    }
  }
}