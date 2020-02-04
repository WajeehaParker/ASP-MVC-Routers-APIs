/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

namespace BearerTokenAuthenticationSample.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private static string aadInstance = "https://login.microsoftonline.com/{0}";
        private static string tenant = ConfigurationManager.AppSettings["AzureAD.Tenant"];
        private static string clientId = ConfigurationManager.AppSettings["AzureAD.ClientId"];
        private static string authority = string.Format(CultureInfo.InvariantCulture, aadInstance, tenant);
        private static AuthenticationContext authContext = null;


        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            authContext = new AuthenticationContext(authority, new TokenCache());
            var creds = new UserPasswordCredential(context.UserName, context.Password);
            AuthenticationResult result;
            try
            {
                result = await authContext.AcquireTokenAsync(clientId, clientId, creds);
            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

            context.Validated(identity);

        }
    }
}*/