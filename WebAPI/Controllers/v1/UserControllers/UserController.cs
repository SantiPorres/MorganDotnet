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

        [HttpGet("all")]
        public async Task<PagedResponse<PagedList<UserDTO>>> GetAllUsers([FromQuery] PaginationQueryParameters filters)
        {
            PagedList<UserDTO> pagedUsers = await _userService.GetAllUsers(filters);
            return new PagedResponse<PagedList<UserDTO>>(
                    pagedUsers,
                    message: null,
                    totalCount: pagedUsers.TotalCount,
                    pagedUsers.PageSize,
                    pagedUsers.CurrentPage,
                    pagedUsers.HasNextPage,
                    pagedUsers.HasPreviousPage,
                    pagedUsers.NextPageNumber,
                    pagedUsers.PreviousPageNumber
            );
        }

        [HttpGet("single")]
        public async Task<Response<UserNavigationDTO>> GetUserById([FromQuery] Guid userId)
        {
            UserNavigationDTO userDto = await _userService.GetUserById(userId);
            return new Response<UserNavigationDTO>(userDto);
        }

        [HttpPost("insert")]
        public async Task<Response<UserDTO>> InsertUser(RegisterUserDTO user)
        {
            UserDTO userDto = await _userService.InsertUser(user);
            return new Response<UserDTO>(userDto);
        }
    }
}
