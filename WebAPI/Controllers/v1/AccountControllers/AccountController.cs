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
using Domain.CustomEntities;

#endregion

namespace WebAPI.Controllers.v1.AccountControllers
{
    public class AccountController : BaseApiController
    {
        //private readonly IAccountService _accountService;

        //public AccountController(IAccountService accountService)
        //{
        //    _accountService = accountService;
        //}

        //[Route("login")]
        //[HttpPost]
        //public async Task<JwtTokenResponse> LoginUser(LoginUserDTO body)
        //{
        //    TokenAndEntity<UserDTO> loggedUser = await _accountService.LoginUser(body);
        //    return new JwtTokenResponse
        //    {
        //        Succeeded = true,
        //        Token = loggedUser.Token,
        //        User = loggedUser.Data
        //    };
        //}

        //[Route("register")]
        //[HttpPost]
        //public async Task<Response<UserDTO>> RegisterUser(RegisterUserDTO body)
        //{
        //    UserDTO userDto = await _accountService.RegisterUser(body);
        //    return new Response<UserDTO>(userDto);
        //}
    }
}
