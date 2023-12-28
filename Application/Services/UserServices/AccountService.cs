#region Usings

// Application
using Application.Interfaces;
using Application.Wrappers;

// Domain
using Domain.CustomExceptions;
using Domain.Entities;

// External libraries
using AutoMapper;
using FluentValidation;
using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Interfaces.ServicesInterfaces;
using Application.Interfaces.UserInterfaces;
using Domain.CustomEntities;

#endregion

namespace Application.Services.UserServices
{
    public class AccountService : IAccountService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _userValidator;
        private readonly IValidator<LoginUserDTO> _loginUserDTOValidator;
        private readonly IValidator<RegisterUserDTO> _registerUserDTOValidator;
        private readonly ITokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public AccountService(
            IPasswordHasher passwordHasher,
            IUserRepository userRepository,
            IValidator<User> userValidator,
            IValidator<LoginUserDTO> loginUserDTOValidator,
            IValidator<RegisterUserDTO> registerUserDTOValidator,
            ITokenService jwtTokenService,
            IMapper mapper
        )
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _userValidator = userValidator;
            _loginUserDTOValidator = loginUserDTOValidator;
            _registerUserDTOValidator = registerUserDTOValidator;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        public async Task<TokenAndEntity<UserDTO>> LoginUser(LoginUserDTO body)
        {
            try
            {
                await _loginUserDTOValidator.ValidateAndThrowAsync(body);
                User userValid = await ValidateCredentials(body);
                string token = await _jwtTokenService.GenerateJWT(userValid);
                UserDTO userDto = _mapper.Map<UserDTO>(userValid);
                return new TokenAndEntity<UserDTO>()
                {
                    Token = token,
                    Data = userDto
                };
            }
            catch (Exception ex) when (
                ex is DataAccessException 
                || ex is FluentValidation.ValidationException 
                || ex is BusinessException
            )
            {
                throw;
            }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }

        public async Task<User> ValidateCredentials(LoginUserDTO body)
        {
            try
            {
                User? user = await _userRepository.GetOneByEmail(body.Email) ?? throw new BusinessException("Credentials not valid");
                bool passwordsMatch = _passwordHasher.Verify(user.Password, body.Password);
                if (passwordsMatch)
                {
                    return user;
                }
                else
                {
                    throw new BusinessException("Credentials not valid");
                }
            }
            catch (Exception ex) when (ex is DataAccessException || ex is BusinessException)
            {
                throw;
            }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }

        public async Task<UserDTO> RegisterUser(RegisterUserDTO body)
        {
            try
            {
                await _registerUserDTOValidator.ValidateAndThrowAsync(body);
                User? emailExists = await _userRepository.GetOneByEmail(body.Email);
                if (emailExists != null)
                    throw new BusinessException("There is already an existing account with this email");
                User user = _mapper.Map<User>(body);
                await _userValidator.ValidateAndThrowAsync(user);
                string hashedPassword = _passwordHasher.Hash(user.Password);
                user.Password = hashedPassword;
                User newUser = await _userRepository.Insert(user);
                UserDTO dto = _mapper.Map<UserDTO>(newUser);
                return dto;
            }
            catch (Exception ex) when (
                ex is DataAccessException 
                || ex is FluentValidation.ValidationException
                || ex is BusinessException
            )
            {
                throw;
            }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }

    }
}
