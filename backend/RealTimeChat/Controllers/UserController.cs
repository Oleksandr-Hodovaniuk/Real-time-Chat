using Microsoft.AspNetCore.Mvc;
using RealTimeChat.Application.Interfaces;

namespace RealTimeChat.Controllers;

public class UserController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    public UserController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;   
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Users.GetAllAsync(includeProperties: "Messages", cancellationToken: cancellationToken);
        foreach (var user in users)
        {
            Console.WriteLine($"User: {user.Id} - {user.Username} - {user.PasswordHash}");
        }
        return Ok();
    }
}
