using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shareds;
namespace WebAppCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User user,
            [FromServices]UserManager<IdentityUser> userManager,
            [FromServices]SignInManager<IdentityUser> signInManager,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations)
        {
            await CheckUserCreatedAsync(userManager);

            if (string.IsNullOrEmpty(user.UserName)) return NotFound(TokenValidate.Create(0, "E-mail invalid"));
            if (string.IsNullOrEmpty(user.Password)) return NotFound(TokenValidate.Create(0, "Password invalid"));

            IdentityUser appUser = await userManager.FindByEmailAsync(user.UserName);

            if (appUser == null) return NotFound(TokenValidate.Create(0, "User not exists"));
            var result = await signInManager.CheckPasswordSignInAsync(appUser, user.Password, false);

            if (!result.Succeeded) return NotFound(TokenValidate.Create(0, "User not credentials"));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new GenericIdentity(appUser.Email, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, appUser.Id),
                        new Claim(JwtRegisteredClaimNames.UniqueName, appUser.Email)
                    }
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = handler.CreateToken(tokenConfigurations, signingConfigurations, claimsIdentity);
            string token = handler.WriteToken(securityToken);

            return Ok(TokenValidate.Create(1, "Login Succeeded", handler.DateCreateToken(), handler.DateExpirationToken(), token));
        }

        [NonAction]
        public async Task CheckUserCreatedAsync(UserManager<IdentityUser> userManager)
        {
            if (await userManager.FindByEmailAsync("fulviocanducci@hotmail.com") == null)
            {
                var result = await userManager.CreateAsync(new IdentityUser
                {
                    Email = "fulviocanducci@hotmail.com",
                    UserName = "fulviocanducci@hotmail.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                }, "Ab@123456");
            }
        }
    }
}