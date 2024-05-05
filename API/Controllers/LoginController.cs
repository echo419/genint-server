using API.Messages;
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

        [HttpGet]
        public LoginResponse Index(string userName, string passwordHash)
        {
            LoginResponse response = new LoginResponse();

            // test / test
            if (userName == "test" && passwordHash == "098f6bcd4621d373cade4e832627b4f6")
            {
                response.Success = true;
                
            }
            else
            {
                response.Error = "Invalid credentials";
            }

            return response;
        }
    }
}
