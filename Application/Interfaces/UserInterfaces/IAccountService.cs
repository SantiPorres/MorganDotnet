using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Wrappers;
using Domain.CustomEntities;
using Domain.Entities;

namespace Application.Interfaces.UserInterfaces
{
    public interface IAccountService
    {

        Task<TokenAndEntity<UserDTO>> LoginUser(LoginUserDTO body);

        Task<User> ValidateCredentials(LoginUserDTO body);

        Task<UserDTO> RegisterUser(RegisterUserDTO body);
    }
}
