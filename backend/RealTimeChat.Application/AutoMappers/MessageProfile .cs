using AutoMapper;
using RealTimeChat.Domain.Dtos;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Application.AutoMappers;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Message, MessageDto>()
            .ForMember(dest => dest.UserId, opt =>
                opt.MapFrom(src => src.UserId.ToString()))
            .ForMember(dest => dest.Text, opt =>
                opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.SentimentType, opt =>
                opt.MapFrom(src => src.SentimentType.ToString()))
            .ForMember(dest => dest.Created, opt =>
                opt.MapFrom(src => src.Created.ToString("HH:mm dd-MM-yyyy ")))
            .ReverseMap();

        CreateMap<ChatMessageDto, Message>()
            .ForMember(dest => dest.Text, opt =>
                opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.Created, opt =>
                opt.MapFrom(src => DateTime.Now));
    }
}
