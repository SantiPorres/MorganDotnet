using Application.DTOs.Account;
using Application.DTOs.User;
using Application.Interfaces;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.v1.Common;

namespace WebAPI.Controllers.v1.Account
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
        public async Task<Response<UserDTO>> RegisterUser(CreateUserDTO body)
        {
            return await _accountService.RegisterUser(body);
        }
    }
}
