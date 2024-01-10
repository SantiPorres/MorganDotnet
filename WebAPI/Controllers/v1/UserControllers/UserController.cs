using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Filters;
using Application.Interfaces.IServices;
using Application.Wrappers;
using Domain.CustomEntities;
using Domain.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.v1.Common;

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
            try
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

        [HttpGet("single")]
        public async Task<Response<UserDTO>> GetUserById([FromQuery] Guid userId)
        {
            try
            {
                UserDTO userDto = await _userService.GetUserById(userId, true);
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

        [HttpPost("add")]
        public async Task<Response<UserDTO>> AddUser(RegisterUserDTO user)
        {
            try
            {
                UserDTO userDto = await _userService.AddUser(user);
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
