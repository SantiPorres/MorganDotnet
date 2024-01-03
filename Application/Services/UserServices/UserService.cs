// Application
using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Filters;
using Application.Interfaces;
using Application.Interfaces.UserInterfaces;
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

namespace Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<User> _userValidator;
        private readonly IValidator<RegisterUserDTO> _registerUserDTOValidator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IOptions<PaginationOptions> _paginationOptions;
        private readonly IMapper _mapper;

        public UserService(
            IValidator<User> userValidator,
            IValidator<RegisterUserDTO> registerUserDTOValidator,
            IOptions<PaginationOptions> paginationOptions,
            IMapper mapper,
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork)
        {
            _userValidator = userValidator;
            _registerUserDTOValidator = registerUserDTOValidator;
            _paginationOptions = paginationOptions;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
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

        public async Task<UserNavigationDTO> GetUserById(Guid userId)
        {
            try
            {
                User user = await _unitOfWork.Users.GetWithNavigationAsync(userId);
                UserNavigationDTO dto = _mapper.Map<UserNavigationDTO>(user);
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
                await _registerUserDTOValidator.ValidateAndThrowAsync(body);
                IEnumerable<User> emailExists = await _unitOfWork.Users.FindAsync(
                    user => user.Email == body.Email
                );
                if (emailExists.Count() > 0)
                    throw new BusinessException("There is already an existing account with this email");
                User user = _mapper.Map<User>(body);
                await _userValidator.ValidateAndThrowAsync(user);
                string hashedPassword = _passwordHasher.Hash(user.Password);
                user.Password = hashedPassword;

                User newUser = _unitOfWork.Users.AddAndGet(user);
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
