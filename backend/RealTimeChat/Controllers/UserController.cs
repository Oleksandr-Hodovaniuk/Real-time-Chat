using Microsoft.AspNetCore.Mvc;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Application.Users.Commands;
using RealTimeChat.Application.Users.Queries;
using RealTimeChat.Domain.Dtos;

namespace RealTimeChat.Controllers;

public class UserController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    public UserController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;   
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(string userId, CancellationToken cancellationToken)
    {
        var user = await Mediator.Send(new GetUserByIdQuery(userId), cancellationToken);

        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto dto, CancellationToken cancellationToken)
    {
        var user = await Mediator.Send(new RegisterUserCommand(dto), cancellationToken);

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginDto dto, CancellationToken cancellationToken)
    {
        var user = await Mediator.Send(new LoginUserCommand(dto), cancellationToken);
        return Ok(user);
    }
}
