using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using RealTimeChat.Application.Dtos;
using RealTimeChat.Application.Exceptions;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Domain.Entities;
using RealTimeChat.Domain.Enums;
using System.Globalization;

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
    public async Task SendMessage(MessageDto dto)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();

        if (!Guid.TryParse(dto.UserId, out var userId))
            throw new BusinessException("Invalid UserId format!");

        if (await _unitOfWork.Users.GetAsync(u => u.Id == userId) == null)
            throw new NotFoundException("User with this Id doesn't exist!");

        dto.SentimentType = SentimentTypeEnum.Neutral.ToString(); // Implement sentiment analysis logic here
        dto.Created = DateTime.Now.ToString("HH:mm dd MMM yyyy ", new CultureInfo("en-US"));
        
        var message = _mapper.Map<Message>(dto);

        _unitOfWork.Messages.Add(message);
        await _unitOfWork.SaveAsync();

        var messageDto = _mapper.Map<MessageDto>(message);

        await transaction.CommitAsync();

        await Clients.All.SendAsync("ReceiveMessage", messageDto);
    }
}
