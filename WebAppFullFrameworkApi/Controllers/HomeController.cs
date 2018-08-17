using System.Web.Http;
using WebAppFullFrameworkApi.Models;

namespace WebAppFullFrameworkApi.Controllers
{
    public class HomeController : ApiController
    {
        public HomeController(IAuthRepository authRepository)
        {
            AuthRepository = authRepository;            
        }

        public IAuthRepository AuthRepository { get; }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(new
            {
                message = "Web Api Full Framework Success"
            });
        }
    }
}
