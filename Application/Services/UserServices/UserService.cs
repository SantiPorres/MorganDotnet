// Application
using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Filters;
using Application.Interfaces;
using Application.Interfaces.IServices;
// Domain
using Domain.CustomEntities;
using Domain.CustomExceptions;
using Domain.Entities;
using Domain.Options;
// Internal libraries
using Microsoft.Extensions.Options;
// External libraries
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IValidator<User> _userValidator;
        private readonly IValidator<RegisterUserDTO> _registerUserDTOValidator;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        private readonly IOptions<PaginationOptions> _paginationOptions;
        private readonly IMapper _mapper;

        public UserService(
            IValidator<User> userValidator,
            IValidator<RegisterUserDTO> registerUserDTOValidator,

            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,

            IOptions<PaginationOptions> paginationOptions,
            IMapper mapper
        )
        {
            _userValidator = userValidator;
            _registerUserDTOValidator = registerUserDTOValidator;

            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;

            _paginationOptions = paginationOptions;
            _mapper = mapper;
        }

        public async Task<PagedList<UserDTO>> GetAllUsers(PaginationQueryParameters filters)
        {
            try
            {
                IEnumerable<User> users = await _unitOfWork.Users.GetAllAsync();
                IEnumerable<UserDTO> dto = _mapper.Map<IEnumerable<UserDTO>>(users);
                PagedList<UserDTO> pagedUsers = PagedList<UserDTO>.Create(
                    dto,
                    filters.PageNumber ?? _paginationOptions.Value.DefaultPageNumber,
                    filters.PageSize ?? _paginationOptions.Value.DefaultPageSize
                );
                return pagedUsers;
            }
            catch (DataAccessException) { throw; }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }

        public async Task<UserDTO> GetUserById(Guid userId, bool? navigable = true)
        {
            try
            {
                User user;
                if (navigable == true)
                    user = await _unitOfWork.Users.GetWithNavigationAsync(userId);
                else
                    user = await _unitOfWork.Users.GetAsync(userId);
                UserDTO dto = _mapper.Map<UserDTO>(user);
                dto.NavigatingUserProjects = navigable;
                return dto;
            }
            catch (Exception ex) when (
                ex is KeyNotFoundException
                || ex is DataAccessException
                || ex is BusinessException
            ) { throw; }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }

        public async Task<UserDTO> AddUser(RegisterUserDTO body)
        {
            try
            {
                // FluentValidation.ValidateAndThrowAsync method is not testeable
                ValidationResult bodyValidationResult = await _registerUserDTOValidator.ValidateAsync(body);
                if (bodyValidationResult.IsValid == false)
                    throw new FluentValidation.ValidationException(
                        bodyValidationResult.Errors
                    );
                IEnumerable<User> emailExists = await _unitOfWork.Users.FindAsync(
                    user => user.Email == body.Email
                );
                if (emailExists.Count() > 0)
                    throw new BusinessException("There is already an existing account with this email");
                User user = _mapper.Map<User>(body);
                ValidationResult userValidationResult = await _userValidator.ValidateAsync(user);
                if (userValidationResult.IsValid == false)
                    throw new FluentValidation.ValidationException(
                        userValidationResult.Errors
                    );
                string hashedPassword = _passwordHasher.Hash(user.Password);
                user.Password = hashedPassword;

                User newUser = await _unitOfWork.Users.AddAndGetAsync(user);
                await _unitOfWork.Complete();
                UserDTO dto = _mapper.Map<UserDTO>(newUser);
                return dto;
            }
            catch (Exception ex) when (
                ex is DataAccessException
                || ex is FluentValidation.ValidationException
                || ex is BusinessException
            ) { throw; }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }
    }
}
