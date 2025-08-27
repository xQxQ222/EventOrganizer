using AutoMapper;
using ModelHolder.Dto;
using ModelHolder.Models;

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
