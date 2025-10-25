using AutoMapper;
using MediatR;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Application.Dtos;

namespace RealTimeChat.Application.Messages.Queries;

public record GetMessagesQuery : IRequest<List<MessageDto>>;
internal class GetMessagesHandler : IRequestHandler<GetMessagesQuery, List<MessageDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetMessagesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        var messages = await _unitOfWork.Messages.GetAllAsync(includeProperties: "User",cancellationToken: cancellationToken);
        
        if (messages == null || !messages.Any())
            return new List<MessageDto>();

        messages = messages.OrderBy(m => m.Created).ToList();

        var messageDtos = _mapper.Map<List<MessageDto>>(messages);

        await transaction.CommitAsync(cancellationToken);

        return messageDtos;
    }
}
