using Application.DTOs.Account;
using Application.DTOs.User;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _userValidator;
        private readonly ITokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public AccountService(
            IPasswordHasher passwordHasher, 
            IUserRepository userRepository, 
            IValidator<User> userValidator, 
            ITokenService jwtTokenService,
            IMapper mapper
        )
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _userValidator = userValidator;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        public async Task<JwtTokenResponse> LoginUser(LoginUserDTO body)
        {
            User userValid = await ValidateCredentials(body);

            try
            {
                string token = await _jwtTokenService.GenerateJWT(userValid);
                return new JwtTokenResponse(
                    succeeded: true,
                    token: token
                );
            }
            catch(Exception)
            {
                throw;
            }
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
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<Response<UserDTO>> RegisterUser(CreateUserDTO body)
        {
            try
            {
                User? emailExists = await _userRepository.GetOneByEmail(body.Email);
                if (emailExists != null)
                {
                    throw new BusinessException("There is already an existing account with this email");
                }

                User user = _mapper.Map<User>(body);
                ValidationResult validationResult = await _userValidator.ValidateAsync(user);
                if (!validationResult.IsValid)
                {
                    throw new Exceptions.ValidationException(validationResult.Errors);
                }
                string hashedPassword = _passwordHasher.Hash(user.Password);
                user.Password = hashedPassword;
                User newUser = await _userRepository.Insert(user);
                UserDTO dto = _mapper.Map<UserDTO>(newUser);
                return new Response<UserDTO>(dto);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

    }
}
