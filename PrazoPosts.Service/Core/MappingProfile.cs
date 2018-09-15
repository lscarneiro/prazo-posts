using System;
using AutoMapper;
using PrazoPosts.Dto;
using PrazoPosts.Model;

namespace PrazoPosts.Service.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
