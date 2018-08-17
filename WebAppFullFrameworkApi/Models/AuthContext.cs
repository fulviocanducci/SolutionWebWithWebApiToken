using Microsoft.AspNet.Identity.EntityFramework;
namespace WebAppFullFrameworkApi.Models
{
    public sealed class AuthContext: IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
    }
}