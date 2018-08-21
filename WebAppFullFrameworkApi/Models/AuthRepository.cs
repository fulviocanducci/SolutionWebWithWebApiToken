using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shareds;
using System.Threading.Tasks;

namespace WebAppFullFrameworkApi.Models
{
    public sealed class AuthRepository : IAuthRepository
    {
        private UserManager<IdentityUser> UserManager { get; }

        public AuthRepository(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(User user)
        {
            IdentityUser model = new IdentityUser
            {
                UserName = user.UserName,
                Email = user.UserName
            };
            return (await UserManager.CreateAsync(model, user.Password));
        }

        public async Task<IdentityUser> FindUserAsync(string name, string password)
        {
            return (await UserManager.FindAsync(name, password));
        }

        public void Dispose()
        {
            UserManager?.Dispose();
            System.GC.SuppressFinalize(this);
        }
    }
}