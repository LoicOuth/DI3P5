using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using USite.Application.Elements.Commands.UpdateElementUrl;
using USite.Application.Elements.Queries.Dto;
using USite.Application.Elements.Queries.GetElementsWithPageIdQuery;
using static USite.Presentation.Hubs.HubElement;
using USite.Presentation.Hubs;

namespace USite.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ElementController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<HubElement, IHubElement> _hubContext;

    public ElementController(IMediator mediator, IHubContext<HubElement, IHubElement> hubContext)
    {
        _mediator = mediator;
        _hubContext = hubContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<ElementDto>>> Get([FromQuery] GetElementsWithPageIdQuery query)
    {
        return await _mediator.Send(query);
    }
    
    [HttpPost]
    public async Task<ActionResult<ElementDto>> Post([FromForm] UpdateElementUrlCommand command, string groupName)
    {
        var element = await _mediator.Send(command);
        await _hubContext.Clients.Group(groupName).UpdateElement(element);

        return element;
    }
}
