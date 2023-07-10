using Microsoft.AspNetCore.SignalR;
using USite.Application.Common.Interfaces;
using USite.Application.Common.Models;

namespace USite.Infrastructure.Hubs;

public class HubDeployment : Hub 
{
    public async Task AddToGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName, Context.ConnectionAborted);
    }

    public async Task RemoveFromGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}

public class HubDeploymentContextWrapper : IHubDeploymentContext
{
    private readonly IHubContext<HubDeployment> _hubContext;

    public HubDeploymentContextWrapper(IHubContext<HubDeployment> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task OnProgress(string groupName, ProgressModel progress)
    {
        await _hubContext.Clients.Group(groupName)
            .SendAsync(HubDeploymentConstants.ON_PROGRESS, progress);
    }
}
