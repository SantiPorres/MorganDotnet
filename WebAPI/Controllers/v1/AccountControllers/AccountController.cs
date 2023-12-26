#region Usings

// Application
using Application.Wrappers;

// WebAPI
using WebAPI.Controllers.v1.Common;

// Internal libraries
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Interfaces.UserInterfaces;

#endregion

namespace WebAPI.Controllers.v1.AccountControllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Route("login")]
        [HttpPost]
        public async Task<JwtTokenResponse> LoginUser(LoginUserDTO body)
        {
            return await _accountService.LoginUser(body);
        }

        [Route("register")]
        [HttpPost]
        public async Task<Response<UserDTO>> RegisterUser(RegisterUserDTO body)
        {
            return await _accountService.RegisterUser(body);
        }
    }
}
