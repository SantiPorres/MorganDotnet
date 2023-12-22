using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region DTOs

            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<User, CreateUserDTO>().ReverseMap();

            CreateMap<User, UpdateUserDTO>().ReverseMap();

            #endregion
        }
    }
}
