using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace WebAppFullFrameworkApi.Models
{
    public class AuthCheck: IDisposable
    {
        private UserManager<IdentityUser> UserManager { get; }

        public AuthCheck()
        {
            UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new AuthContext()));
        }

        public async Task<IdentityUser> FindUserAsync(string name, string passwrod)
        {
            return await UserManager.FindAsync(name, passwrod);
        }

        public void Dispose()
        {
            UserManager?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}