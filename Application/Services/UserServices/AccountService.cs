// Application
using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Application.Interfaces.IServices;
// External libraries
using AutoMapper;
using FluentValidation;
// Domain
using Domain.CustomEntities;
using Domain.CustomExceptions;
using Domain.Entities;
using FluentValidation.Results;


namespace Application.Services.UserServices
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IValidator<LoginUserDTO> _loginUserDTOValidator;
        private readonly ITokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AccountService(
            IPasswordHasher passwordHasher,
            IValidator<LoginUserDTO> loginUserDTOValidator,
            ITokenService jwtTokenService,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserService userService
        )
        {
            _passwordHasher = passwordHasher;
            _loginUserDTOValidator = loginUserDTOValidator;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<TokenAndEntity<UserDTO>> LoginUser(LoginUserDTO body)
        {
            try
            {
                ValidationResult validationResult = await _loginUserDTOValidator.ValidateAsync(body);
                if (validationResult.IsValid == false)
                    throw new FluentValidation.ValidationException(
                        validationResult.Errors
                    );
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
            { throw; }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }

        public async Task<User> ValidateCredentials(LoginUserDTO body)
        {
            try
            {
                IEnumerable<User> usersWithEmail = await _unitOfWork.Users.FindAsync(
                    user => user.Email == body.Email
                );
                if (usersWithEmail.Count() < 1)
                    throw new BusinessException("The email is not registered");
                User user = usersWithEmail.First();
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
            catch (Exception ex) when (
                ex is DataAccessException
                || ex is BusinessException
            )
            { throw; }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }

        public async Task<UserDTO> RegisterUser(RegisterUserDTO body)
        {
            try
            {
                return await _userService.AddUser(body);
            }
            catch (Exception ex) when (
                ex is DataAccessException
                || ex is FluentValidation.ValidationException
                || ex is BusinessException
            )
            { throw; }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }

    }
}