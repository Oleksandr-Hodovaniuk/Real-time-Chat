using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using RealTimeChat.Application.Dtos;
using RealTimeChat.Application.Exceptions;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Hubs;

public class ChatHub : Hub
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITextAnalyticsService _textAnalyticsService;
    public ChatHub(IUnitOfWork unitOfWork, IMapper mapper, ITextAnalyticsService textAnalyticsService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _textAnalyticsService = textAnalyticsService;
    }
    public async Task SendMessage(MessageDto dto)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();

        if (!Guid.TryParse(dto.UserId, out var userId))
            throw new BusinessException("Invalid UserId format!");

        if (await _unitOfWork.Users.GetAsync(u => u.Id == userId) == null)
            throw new NotFoundException("User with this Id doesn't exist!");

        var message = new Message 
        {
            UserId = userId,
            Text = dto.Text,
            Created = DateTime.Now,
            SentimentType = await _textAnalyticsService.AnalyzeSentimentAsync(dto.Text)
        };

        _unitOfWork.Messages.Add(message);
        await _unitOfWork.SaveAsync();

        dto.SentimentType = message.ToString()!;
        dto.Created = DateTime.Now.ToString();

        var messageDto = _mapper.Map<MessageDto>(message);

        await transaction.CommitAsync();

        await Clients.All.SendAsync("ReceiveMessage", messageDto);
    }
}
