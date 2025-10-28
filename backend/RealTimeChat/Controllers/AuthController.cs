using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.Application.Users.Commands;
using RealTimeChat.Application.Users.Queries;
using RealTimeChat.Application.Dtos;

namespace RealTimeChat.Controllers;

public class AuthController : BaseController
{
    private readonly IValidator<UserRegisterDto> _registerValidator;
    private readonly IValidator<UserLoginDto> _loginValidator;
    public AuthController(IValidator<UserRegisterDto> registerValidator, IValidator<UserLoginDto> loginValidator)
    {
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
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
        var validationResult = await _registerValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var user = await Mediator.Send(new RegisterUserCommand(dto), cancellationToken);

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await _loginValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var user = await Mediator.Send(new LoginUserCommand(dto), cancellationToken);

        return Ok(user);
    }
}
