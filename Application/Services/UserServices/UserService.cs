#region Usings

// Application
using Application.Filters;
using Application.Wrappers;

// Domain
using Domain.CustomExceptions;
using Domain.CustomEntities;
using Domain.Entities;
using Domain.Options;

// External libraries
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Options;
using Application.DTOs.UserDTOs;
using Application.Interfaces.UserInterfaces;
using Application.DTOs.AccountDTOs;
using Application.Validators.AccountValidators;
using Application.Interfaces;
using Application.DTOs.ProjectDTOs;

#endregion

namespace Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _userValidator;
        private readonly IValidator<RegisterUserDTO> _registerUserDTOValidator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IOptions<PaginationOptions> _paginationOptions;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IValidator<User> userValidator,
            IValidator<RegisterUserDTO> registerUserDTOValidator,
            IOptions<PaginationOptions> paginationOptions,
            IMapper mapper
,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
            _registerUserDTOValidator = registerUserDTOValidator;
            _paginationOptions = paginationOptions;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<PagedList<UserDTO>> GetAllUsers(PaginationQueryParameters filters)
        {
            try
            {
                IEnumerable<User> users = await _userRepository.GetAllUsers(navigateUserProjects: false);
                IEnumerable<UserDTO> dto = _mapper.Map<IEnumerable<UserDTO>>(users);
                PagedList<UserDTO> pagedUsers = PagedList<UserDTO>.Create(
                    dto,
                    filters.PageNumber ?? _paginationOptions.Value.DefaultPageNumber,
                    filters.PageSize ?? _paginationOptions.Value.DefaultPageSize
                );
                return pagedUsers;
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

        public async Task<UserNavigationDTO> GetUserById(Guid userId)
        {
            try
            {
                User user = await _userRepository.GetOneById(
                    userId, 
                    navigateUserProjects: true
                ) ?? throw new KeyNotFoundException("User not found");
                UserNavigationDTO dto = _mapper.Map<UserNavigationDTO>(user);
                return dto;
            }
            catch (Exception ex) when (
                ex is DataAccessException 
                || ex is BusinessException
            )
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"An error occurred: {ex.Message}");
            }
        }

        public async Task<UserDTO> InsertUser(RegisterUserDTO body)
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
