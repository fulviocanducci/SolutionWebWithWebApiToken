using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebAppFullFrameworkApi.Models
{
    public interface IAuthRepository: System.IDisposable
    {
        Task<IdentityUser> FindUserAsync(string name, string password);
        Task<IdentityResult> RegisterUserAsync(User user);
    }
}