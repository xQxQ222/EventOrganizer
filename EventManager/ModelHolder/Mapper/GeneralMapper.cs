using AutoMapper;
using ModelHolder.Dto;
using ModelHolder.Models;
using UtilityHolder.Dto;

namespace EventManager.Mapper
{
    public class GeneralMapper : Profile
    {
        public GeneralMapper()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Location, LocationDto>();
            CreateMap<LocationDto, Location>();
            CreateMap<Image, ImageDto>();
            CreateMap<ImageDto, Image>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<Request, RequestDto>();
            CreateMap<RequestDto, RequestDto>();
            CreateMap<Event, EventDto>();
            CreateMap<EventDto, EventDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();
        }
    }
}
