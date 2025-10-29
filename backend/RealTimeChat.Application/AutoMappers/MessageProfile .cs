using AutoMapper;
using RealTimeChat.Application.Dtos;
using RealTimeChat.Domain.Entities;
using System.Globalization;

namespace RealTimeChat.Application.AutoMappers;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Message, MessageDto>()
            .ForMember(dest => dest.UserId, opt =>
                opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.Username, opt =>
                opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.Text, opt =>
                opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.SentimentType, opt =>
                opt.MapFrom(src => src.SentimentType.ToString()))
            .ForMember(dest => dest.Created, opt =>
                opt.MapFrom(src => src.Created.ToString("HH:mm dd MMM yyyy ", new CultureInfo("en-US"))));

        CreateMap<MessageDto, Message>()
            .ForMember(dest => dest.UserId, opt =>
                opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Text, opt =>
                opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.SentimentType, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt =>
                opt.MapFrom(src => DateTime.Now));
    }
}
