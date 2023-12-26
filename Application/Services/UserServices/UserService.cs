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

#endregion

namespace Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _userValidator;
        private readonly IOptions<PaginationOptions> _paginationOptions;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IValidator<User> userValidator,
            IOptions<PaginationOptions> paginationOptions,
            IMapper mapper
        )
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
            _paginationOptions = paginationOptions;
            _mapper = mapper;
        }

        public async Task<PagedResponse<PagedList<UserDTO>>> GetAllUsers(PaginationQueryParameters filters)
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                var dto = _mapper.Map<IEnumerable<UserDTO>>(users);
                var pagedUsers = PagedList<UserDTO>.Create(
                    dto,
                    filters.PageNumber ?? _paginationOptions.Value.DefaultPageNumber,
                    filters.PageSize ?? _paginationOptions.Value.DefaultPageSize
                );
                return new PagedResponse<PagedList<UserDTO>>(
                    pagedUsers,
                    message: null,
                    totalCount: pagedUsers.TotalCount,
                    pagedUsers.PageSize,
                    pagedUsers.CurrentPage,
                    pagedUsers.HasNextPage,
                    pagedUsers.HasPreviousPage,
                    pagedUsers.NextPageNumber,
                    pagedUsers.PreviousPageNumber
                );
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

        public async Task<Response<UserDTO>> InsertUser(RegisterUserDTO body)
        {
            try
            {
                var user = _mapper.Map<User>(body);
                var validationResult = await _userValidator.ValidateAsync(user);
                if (!validationResult.IsValid)
                {
                    throw new Domain.CustomExceptions.ValidationException(validationResult.Errors);
                }
                var newUser = await _userRepository.Insert(user);
                UserDTO dto = _mapper.Map<UserDTO>(newUser);
                return new Response<UserDTO>(dto);
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
    }
}
