using Application.DTOs.User;
using Application.Filters;
using Application.Interfaces;
using Application.Wrappers;
using Domain.CustomEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.v1.Common;

namespace WebAPI.Controllers.v1.User
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
        public async Task<Response<UserDTO>> InsertUser(CreateUserDTO user)
        {
            return await _userService.InsertUser(user);
        }
    }
}
