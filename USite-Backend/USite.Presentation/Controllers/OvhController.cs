using MediatR;
using Microsoft.AspNetCore.Mvc;
using USite.Application.Ovh.Commands.CreateSubdomainCommand;
using USite.Application.Ovh.Queries.GetSubdomainAvailabilityQuery;

namespace USite.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OvhController : ControllerBase
{
    private readonly IMediator _mediator;

    public OvhController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("subdomain/check")]
    public async Task<ActionResult<bool>> GetSubdomainAvailability([FromQuery] GetSubdomainAvailabilityQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpPost("subdomain/create")]
    public async Task<ActionResult<Unit>> CreateSubdomain(CreateSubdomainCommand command)
    {
        return await _mediator.Send(command);
    }
}
