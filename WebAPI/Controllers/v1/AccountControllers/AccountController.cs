using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Interfaces.IServices;
using Application.Wrappers;
using Domain.CustomEntities;
using Domain.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.v1.Common;

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
            try
            {
                TokenAndEntity<UserDTO> loggedUser = await _accountService.LoginUser(body);
                return new JwtTokenResponse
                {
                    Succeeded = true,
                    Token = loggedUser.Token,
                    User = loggedUser.Data
                };
            }
            catch (Exception ex) when (
                ex is DataAccessException
                || ex is UnauthorizedAccessException
                || ex is FluentValidation.ValidationException
                || ex is BusinessException
                || ex is KeyNotFoundException
            )
            { throw; }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
        }

        [Route("register")]
        [HttpPost]
        public async Task<Response<UserDTO>> RegisterUser(RegisterUserDTO body)
        {
            try
            {
                UserDTO userDto = await _accountService.RegisterUser(body);
                return new Response<UserDTO>(userDto);
            }
            catch (Exception ex) when (
                ex is DataAccessException
                || ex is UnauthorizedAccessException
                || ex is FluentValidation.ValidationException
                || ex is BusinessException
                || ex is KeyNotFoundException
            )
            { throw; }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
        }
    }
}
