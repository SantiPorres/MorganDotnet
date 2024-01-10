using Application.DTOs;
using Application.DTOs.AccountDTOs;
using Application.DTOs.AssignmentDTOs;
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

            // Input
            CreateMap<RegisterUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();

            #endregion

            #region Project

            CreateMap<Project, ProjectDTO>().ReverseMap();

            // Input
            CreateMap<CreateProjectDTO, Project>();

            #endregion

            #region UserProject

            CreateMap<UserProject, UserProjectDTO>();

            #endregion

            #region Assignment

            CreateMap<Assignment, AssignmentDTO>().ReverseMap();

            // Input
            CreateMap<CreateAssignmentDTO, Assignment>();
            
            #endregion
        }
    }
}
