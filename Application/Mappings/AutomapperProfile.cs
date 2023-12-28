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
            #region UserDTOs

            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<User, UserNavigationDTO>().ReverseMap();

            CreateMap<User, RegisterUserDTO>().ReverseMap();

            CreateMap<User, UpdateUserDTO>().ReverseMap();

            #endregion

            #region ProjectDTOs

            CreateMap<Project, ProjectDTO>().ReverseMap();

            CreateMap<Project, ProjectNavigationDTO>().ReverseMap();

            CreateMap<Project, CreateProjectDTO>().ReverseMap();

            #endregion
        }
    }
}
