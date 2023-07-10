using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using USite.Application.Common.Models;
using USite.Application.Elements.Queries.Dto;
using USite.Application.Template.Commands.AddTemplateBlockInSiteCommand;
using USite.Application.Template.Commands.ImportHtmlCommand;
using USite.Application.Template.Queries.GetAllTemplateQuery;
using USite.Presentation.Hubs;
using static USite.Presentation.Hubs.HubElement;

namespace USite.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TemplateController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<HubElement, IHubElement> _hubContext;

    public TemplateController(IMediator mediator, IHubContext<HubElement, IHubElement> hubContext)
    {
        _mediator = mediator;
        _hubContext = hubContext;
    }

    [HttpPost("import")]
    public async Task<ActionResult> ImportHtml (ImportTemplateBlockCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<ElementDto>>> GetAllTemplate([FromQuery] GetAllTemplateBlockQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpPost("{templateId}/page/{pageId}/add")]
    public async Task<ActionResult> AddTemplateBlockToPage(AddTemplateBlockToPageCommand command, Guid templateId, Guid pageId)
    {
        if (command.PageId != pageId || command.TemplateId != templateId)
            return BadRequest();

        var templateDto = await _mediator.Send(command);
        await _hubContext.Clients.Group(templateDto.SiteId.ToString()).AddNewElement(templateDto.Elements);

        return Ok();
    }
}
