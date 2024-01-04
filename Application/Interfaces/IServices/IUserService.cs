﻿using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Filters;
using Domain.CustomEntities;

namespace Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<PagedList<UserDTO>> GetAllUsers(PaginationQueryParameters filters);

        Task<UserDTO> GetUserById(Guid userId, bool? navigable = true);

        Task<UserDTO> AddUser(RegisterUserDTO body);
    }
}
