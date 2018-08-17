using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;
using WebAppFullFrameworkApi.Models;

namespace WebAppFullFrameworkApi.Controllers
{
    public class AccountController : ApiController
    {
        public IAuthRepository AuthRepository { get; }

        public AccountController(IAuthRepository authRepository)
        {
            AuthRepository = authRepository;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                AuthRepository?.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]        
        public async Task<IHttpActionResult> Post(User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await AuthRepository.RegisterUserAsync(model);

            return Ok(new { result, model });
        }
    }
}
