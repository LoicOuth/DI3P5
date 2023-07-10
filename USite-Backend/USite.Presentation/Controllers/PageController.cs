using MediatR;
using Microsoft.AspNetCore.Mvc;
using USite.Application.Pages.Commands.CreatePage;
using USite.Application.Pages.Commands.UpdatePage;
using USite.Application.Pages.Query.Dto;
using USite.Application.Pages.Query.GetAllPageWithSiteId;

namespace USite.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<PageDto>>> Get([FromQuery] GetPagesWithSiteIdQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost]
        public async Task<ActionResult<PageDto>> Create(CreatePageCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(UpdatePageCommand command, Guid id)
        {
            if (id != command.PageId)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
