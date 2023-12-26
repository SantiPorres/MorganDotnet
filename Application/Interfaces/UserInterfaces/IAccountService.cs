using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Interfaces.UserInterfaces
{
    public interface IAccountService
    {

        Task<JwtTokenResponse> LoginUser(LoginUserDTO body);

        Task<User> ValidateCredentials(LoginUserDTO body);

        Task<Response<UserDTO>> RegisterUser(RegisterUserDTO body);
    }
}
