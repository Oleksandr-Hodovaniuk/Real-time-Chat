using Microsoft.AspNetCore.Mvc;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Application.Users.Queries;

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
}
