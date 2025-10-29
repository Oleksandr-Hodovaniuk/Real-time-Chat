using Microsoft.AspNetCore.Mvc;
using RealTimeChat.Application.Messages.Queries;

namespace RealTimeChat.Controllers;

public class MessagesController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetMessages(CancellationToken cancellationToken)
    {
        var messages = await Mediator.Send(new GetMessagesQuery(), cancellationToken);

        return Ok(messages);
    }
}
