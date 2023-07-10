using MediatR;
using Microsoft.AspNetCore.Mvc;
using USite.Application.Users.Commands.GetUserInfo;
using USite.Application.Users.Commands.SendEmail;
using USite.Application.Users.Commands.UpdatePassword;
using USite.Application.Users.Commands.UpdatePersonalInfo;
using USite.Application.Users.Queries.DownloadPersonalData;

namespace USite.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> GetUserInfo()
    {
        var result = await _mediator.Send(new GetUserInfoCommand());
        return Ok(result);
    }

    [HttpGet("download")]
    public async Task<ActionResult> DownloadPersonalData()
    {
        var resut = await _mediator.Send(new DownloadPersonalDataCommand());
        return Ok(resut);
    }

    [HttpPost("email")]
    public async Task<ActionResult> SendEmail(SendEmailCommand command)
    {
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> UpdatePersonalInfo(UpdatePersonalInfoCommand command)
    {
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPut("password")]
    public async Task<ActionResult> UpdatePassword(UpdatePasswordCommand command)
    {
        await _mediator.Send(command);

        return NoContent();
    }
}
