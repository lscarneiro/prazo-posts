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
            CreateMap<User, UserDTO>().ForMember(x => x.Password, opt => opt.Ignore());
            CreateMap<UserDTO, User>();

            CreateMap<Author, AuthorDTO>();
            CreateMap<AuthorDTO, Author>();

            CreateMap<BlogPost, BlogPostDTO>();
            CreateMap<BlogPostDTO, BlogPost>();
        }
    }
}
