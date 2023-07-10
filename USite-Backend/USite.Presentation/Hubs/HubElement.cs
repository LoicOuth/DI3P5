using MediatR;
using Microsoft.AspNetCore.SignalR;
using USite.Application.Elements.Commands.CreateElement;
using USite.Application.Elements.Commands.DeleteElement;
using USite.Application.Elements.Commands.UpdateElementContent;
using USite.Application.Elements.Commands.UpdateElementPosition;
using USite.Application.Elements.Commands.UpdateElementStyle;
using USite.Application.Elements.Queries.Dto;
using USite.Application.Pages.Commands.UpdatePage;
using USite.Application.Pages.Query.Dto;
using USite.Application.Menus.Commands.CreateLinkCommand;
using USite.Application.Menus.Commands.UpdateLinkPositionCommand;
using USite.Application.Menus.Query.Dto;
using static USite.Presentation.Hubs.HubElement;

namespace USite.Presentation.Hubs;

public class HubElement : Hub<IHubElement>
{
    private readonly IMediator _mediator;

    public HubElement(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Page
    public async Task UpdatePageInformations(UpdatePageCommand command, string groupName)
    {
        var page = await _mediator.Send(command);
        await Clients.Group(groupName).UpdatePage(page);

    }
    #endregion

    #region Style
    public async Task UpdateElementStyle(UpdateElementStyleCommand command, string groupName)
    {
        var element = await _mediator.Send(command);
        await Clients.Group(groupName).UpdateElement(element);
    }
    #endregion

    #region Element
    public async Task AddElement(CreateElementCommand command, string groupName)
    {
        var element = await _mediator.Send(command);

        await Clients.Group(groupName).AddNewElement(element);
    }
    public async Task DeleteElement(DeleteElementCommand command, string groupName)
    {
        var elementId = await _mediator.Send(command);

        await Clients.Group(groupName).DeleteMyElement(elementId);
    }

    public async Task UpdateElementContent(UpdateElementContentCommand command, string groupName)
    {
        var element = await _mediator.Send(command);

        await Clients.Group(groupName).UpdateElement(element);
    }

    public async Task UpdateElementPosition(UpdateElementPositionCommand command, string groupName)
    {
        var elements = await _mediator.Send(command);

        await Clients.Group(groupName).UpdateElements(elements);
    }

    #endregion
    
    #region ManageGroup
    public async Task AddToGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName, Context.ConnectionAborted);

        await Clients.Group(groupName).UserJoined(Context.ConnectionId);
    }

    public async Task RemoveFromGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        await Clients.Group(groupName).UserLeft(Context.ConnectionId);
    }
    #endregion

    #region Menu
    public async Task AddLink(CreateLinkCommand command, string groupName)
    {
        var element = await _mediator.Send(command);

        await Clients.Group(groupName).UpdateElements(element);
    }
    
    public async Task UpdateLinkPosition(UpdateLinkPositionCommand command, string groupName)
    {
        var element = await _mediator.Send(command);

        await Clients.Group(groupName).UpdateElements(element);
    }
    #endregion
    public interface IHubElement
    {
        Task AddNewElement(ElementDto element);
        Task DeleteMyElement(Guid elementId);
        Task UpdateElement(ElementDto element);
        Task UpdateElements(List<ElementDto> elements);
        Task UserJoined(string connectionId);
        Task UserLeft(string connectionId);
        Task UpdatePage(PageDto page);
        Task AddNewLink(Unit response);
        Task RefreshMenu(MenuDto menu);
        
    }

}