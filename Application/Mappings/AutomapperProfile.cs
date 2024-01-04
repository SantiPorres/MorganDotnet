using Application.DTOs;
using Application.DTOs.AccountDTOs;
using Application.DTOs.ProjectDTOs;
using Application.DTOs.UserDTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            #region User

            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<User, RegisterUserDTO>().ReverseMap();

            CreateMap<User, UpdateUserDTO>().ReverseMap();

            #endregion

            #region Project

            CreateMap<Project, ProjectDTO>().ReverseMap();

            CreateMap<Project, CreateProjectDTO>().ReverseMap();

            #endregion

            #region UserProject

            CreateMap<UserProject, UserProjectDTO>();

            #endregion
        }
    }
}
