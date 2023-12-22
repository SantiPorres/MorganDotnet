using Application.DTOs.User;
using Application.Filters;
using Application.Wrappers;
using Domain.CustomEntities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<PagedResponse<PagedList<UserDTO>>> GetAllUsers(PaginationQueryParameters filters);

        Task<Response<UserDTO>> InsertUser(CreateUserDTO body);
    }
}
