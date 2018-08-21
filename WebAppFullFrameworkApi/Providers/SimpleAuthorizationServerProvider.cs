using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAppFullFrameworkApi.Models;
namespace WebAppFullFrameworkApi.Providers
{
    public sealed class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {            
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            //
            using (AuthCheck auth = new AuthCheck())
            {                
                IdentityUser user = await auth.FindUserAsync(context.UserName, context.Password);
                if (user != null)
                {
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                    claimsIdentity.AddClaim(new Claim("sub", context.UserName));
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, context.UserName));
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "user"));                                        

                    context.Validated(claimsIdentity);                    
                }
                else
                {
                    context.SetError("invalid_grant", "The name and password is incorret.");
                }             
            }
        }
    }
}