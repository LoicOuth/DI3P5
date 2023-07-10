using MediatR;
using Microsoft.AspNetCore.Mvc;
using USite.Application.Menus.Query.Dto;
using USite.Application.Menus.Query.GetMenuWithSiteId;

namespace USite.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMediator _mediator;

	public MenuController(IMediator mediator)
	{
		_mediator= mediator;
	}

    [HttpGet]
    public async Task<ActionResult<MenuDto>> Get([FromQuery] GetMenuWithSiteIdQuery query)
    {
        return await _mediator.Send(query);
    }
}
