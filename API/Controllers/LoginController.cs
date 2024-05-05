using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {

        [HttpGet]
        [Route("[action]")]
        public string Test()
        {
            return "test response";
        }
    }
}
