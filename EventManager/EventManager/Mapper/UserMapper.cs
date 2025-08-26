using AutoMapper;
using ModelHolder.Models;
using UtilityHolder.Dto;

namespace EventManager.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
