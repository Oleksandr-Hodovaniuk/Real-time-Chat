using AutoMapper;
using RealTimeChat.Domain.Dtos;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Application.AutoMappers;

internal class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Username, opt =>
                opt.MapFrom(src => src.Username));

        CreateMap<UserRegisterDto, User>()
            .ForMember(dest => dest.Username, opt =>
                opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.PasswordHash, opt =>
                opt.MapFrom(src => src.Password));
    }
}
