using API.Messages;
using API.Messages.Exceptions;
using API.Services;
using Core;
using Core.Models;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Authentication;

namespace API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {


        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ServiceBase<User, UserView> _userService;
        protected readonly AppContentElementService _appContentElementService;

        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userService = new ServiceBase<User, UserView>(_unitOfWork);
            _appContentElementService = new AppContentElementService(_unitOfWork);
        }

        [HttpGet]
        [Route("[action]")]
        public string Test()
        {
            return "test response";
        }

        [HttpGet]
        public async Task<ResponseBase<ApiAppContentElementView>> Index(string userName, string passwordHash)
        {
            ResponseBase<ApiAppContentElementView> response = new() { };

            try
            {
                await AuthenticateAsync(userName, passwordHash);

                ApiAppContentElementView appContent = await _appContentElementService.GetTreeStructure();
                response.Record = appContent;

                response.Success = true;
            }
            catch (Exception ex)
            {
                // success false by default
                response.Message = ex.Message;
            }

            return response;
        }

        private async Task<UserView> AuthenticateAsync(string userName, string passwordHash)
        {
            if (userName.IsNullOrEmpty()) throw new ApiInvalidCredentialsException();
            if (passwordHash.IsNullOrEmpty()) throw new ApiInvalidCredentialsException();

            UserView? userView = null;

            userView = await _userService.FindByAsync(u => u.UserName == userName);
            if (userView == null) 
            {
                throw new ApiInvalidCredentialsException();
            }

            if (userView.PasswordHash != passwordHash)
            {
                throw new ApiInvalidCredentialsException();
            }

            return userView!;
        }
    }
}
