﻿using AutoMapper;
using BaseProject.Domain.DTO.UserDTO;
using BaseProject.Domain.Identity;

namespace BaseProject.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, LoginUserDTO>().ReverseMap();

            #endregion
        }
    }
}
