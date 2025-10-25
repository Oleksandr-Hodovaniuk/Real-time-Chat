using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RealTimeChat.Application.Exceptions;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Application.Dtos;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Application.Users.Commands;


public record RegisterUserCommand(UserRegisterDto dto) : IRequest<UserDto>;
internal class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RegisterUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        if (request.dto.Password != request.dto.ConfirmPassword)
            throw new BusinessException("Password and Confirm Password do not match!");

        if (await _unitOfWork.Users
            .GetAsync(u => u.Username == request.dto.Username,
            cancellationToken: cancellationToken) != null)
            throw new BusinessException("This Username is already in use!");
        
        var user = _mapper.Map<User>(request.dto);
 
        user.PasswordHash = new PasswordHasher<User>()
            .HashPassword(user, request.dto.Password);

        _unitOfWork.Users.Add(user);
        await _unitOfWork.SaveAsync(cancellationToken);

        var userDto = _mapper.Map<UserDto>(user);

        await transaction.CommitAsync(cancellationToken);

        return userDto;
    }
}
