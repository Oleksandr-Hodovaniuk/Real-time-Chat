using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RealTimeChat.Application.Exceptions;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Application.Dtos;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Application.Users.Commands;

public record LoginUserCommand(UserLoginDto dto) : IRequest<UserDto>;
public class LoginUserhandler : IRequestHandler<LoginUserCommand, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public LoginUserhandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        var user = await _unitOfWork.Users
            .GetAsync(u => u.Username == request.dto.Username,
            cancellationToken: cancellationToken);
        if (user == null)
            throw new NotFoundException("Invalid username!");

        var passResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.dto.Password);
        if (passResult == PasswordVerificationResult.Failed)
            throw new BusinessException("The password is incorrect!");

        var userDto = _mapper.Map<UserDto>(user);

        await transaction.CommitAsync(cancellationToken);

        return userDto;
    }
}
