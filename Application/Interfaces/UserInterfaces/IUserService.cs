using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Filters;
using Application.Wrappers;
using Domain.CustomEntities;

namespace Application.Interfaces.UserInterfaces
{
    public interface IUserService
    {
        Task<PagedList<UserDTO>> GetAllUsers(PaginationQueryParameters filters);

        Task<UserDTO> GetUserById(int id);

        Task<UserDTO> InsertUser(RegisterUserDTO body);
    }
}
