#region Usings

// Application
using Application.Filters;
using Application.Wrappers;

// Domain
using Domain.CustomEntities;

// WebAPI
using WebAPI.Controllers.v1.Common;

// Internal libraries
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.UserDTOs;
using Application.Interfaces.UserInterfaces;
using Application.DTOs.AccountDTOs;

#endregion

namespace WebAPI.Controllers.v1.UserControllers
{
    [Authorize]
    [Produces("application/json")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<PagedResponse<PagedList<UserDTO>>> GetAllUsers([FromQuery] PaginationQueryParameters filters)
        {
            return await _userService.GetAllUsers(filters);
        }

        [HttpPost]
        public async Task<Response<UserDTO>> InsertUser(RegisterUserDTO user)
        {
            return await _userService.InsertUser(user);
        }
    }
}
