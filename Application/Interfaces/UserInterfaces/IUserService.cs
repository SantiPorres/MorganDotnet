using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Filters;
using Domain.CustomEntities;

namespace Application.Interfaces.UserInterfaces
{
    public interface IUserService
    {
        Task<PagedList<UserDTO>> GetAllUsers(PaginationQueryParameters filters);

        Task<UserNavigationDTO> GetUserById(Guid userId);

        Task<UserDTO> AddUser(RegisterUserDTO body);
    }
}
