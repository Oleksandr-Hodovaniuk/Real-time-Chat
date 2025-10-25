using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using RealTimeChat.Application.Exceptions;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Application.Dtos;
using RealTimeChat.Domain.Entities;
using RealTimeChat.Domain.Enums;

namespace RealTimeChat.Hubs;

public class ChatHub : Hub
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ChatHub(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task SendMessage(ChatMessageDto dto)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();

        if (!Guid.TryParse(dto.UserId, out var userId))
            throw new BusinessException("Invalid UserId format!");

        var user = await _unitOfWork.Users.GetAsync(u => u.Id == userId);
        if (user == null)
            throw new NotFoundException("User with this Id doesn't exist!");

        var messageEntity = _mapper.Map<Message>(dto);
        messageEntity.UserId = userId;
        messageEntity.Created = DateTime.Now;
        messageEntity.SentimentType = SentimentTypeEnum.Neutral; // Implement sentiment analysis logic here

        _unitOfWork.Messages.Add(messageEntity);
        await _unitOfWork.SaveAsync();

        var messageDto = _mapper.Map<MessageDto>(messageEntity);

        await transaction.CommitAsync();

        await Clients.All.SendAsync("ReceiveMessage", messageDto);
    }
}
