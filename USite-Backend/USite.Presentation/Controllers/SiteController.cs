using MediatR;
using Microsoft.AspNetCore.Mvc;
using USite.Application.Sites.Commands.CreateSite;
using USite.Application.Sites.Commands.DeleteSite;
using USite.Application.Sites.Commands.FollowDeployment;
using USite.Application.Sites.Commands.NewDeployment;
using USite.Application.Sites.Commands.UpdateSite;
using USite.Application.Sites.Queries;
using USite.Application.Sites.Queries.GetLastDeploymentQuery;
using USite.Application.Sites.Queries.GetSiteFromIdQuery;
using USite.Application.Sites.Queries.GetSites;

namespace USite.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SiteController : ControllerBase
{
    private readonly IMediator _mediator;

    public SiteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<SiteDto>>> Get([FromQuery] GetSitesQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SiteDto>> GetSiteFromId(Guid id)
    {
        return await _mediator.Send(new GetSiteFromIdQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateSiteCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteSiteCommand(id));

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateSiteCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPost("{id}/deploy")]
    public async Task<ActionResult> Deploy(Guid id, NewDeploymentCommand command)
    {
        if (id != command.SiteId)
        {
            return BadRequest();
        }

        return Ok(await _mediator.Send(command));
    }

    [HttpGet("deploy")]
    public async Task<ActionResult<List<BuildDto>>> GetDeployment([FromQuery] int? count)
    {
        return Ok(await _mediator.Send(new GetLastDeploymentQuery(count)));
    }

    [HttpPost("deploy")]
    public async Task<ActionResult> FollowDeployment()
    {
        using var reader = new StreamReader(Request.Body);
        var requestBody = await reader.ReadToEndAsync();

        await _mediator.Send(new FollowDeploymentCommand(requestBody));

        return NoContent();
    }
}
