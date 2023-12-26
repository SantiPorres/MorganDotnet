using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Filters;
using Application.Wrappers;
using Domain.CustomEntities;

namespace Application.Interfaces.UserInterfaces
{
    public interface IUserService
    {
        Task<PagedResponse<PagedList<UserDTO>>> GetAllUsers(PaginationQueryParameters filters);

        Task<Response<UserDTO>> InsertUser(RegisterUserDTO body);
    }
}
