using Application.DTOs.Account;
using Application.DTOs.User;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAccountService
    {

        Task<JwtTokenResponse> LoginUser(LoginUserDTO body);

        Task<User> ValidateCredentials(LoginUserDTO body);

        Task<Response<UserDTO>> RegisterUser(CreateUserDTO body);
    }
}
