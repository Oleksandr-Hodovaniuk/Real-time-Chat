using AutoMapper;
using MediatR;
using RealTimeChat.Application.Exceptions;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Application.Dtos;

namespace RealTimeChat.Application.Users.Queries;

public record GetUserByIdQuery(string UserId) : IRequest<UserDto>;
internal class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        var user = await _unitOfWork.Users.GetAsync(u => u.Id.ToString() == request.UserId, cancellationToken: cancellationToken);
        if (user == null)
            throw new NotFoundException("User with this id doesn't exist!");
        
        var userDto = _mapper.Map<UserDto>(user);

        await transaction.CommitAsync(cancellationToken);

        return userDto;
    }
}
