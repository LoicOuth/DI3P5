using USite.Application.Elements.Helpers;
using USite.Application.Elements.Queries.Dto;
using USite.Application.Sites.Helpers;

namespace USite.Application.Sites.Commands.NewDeployment;

[Authorize]
public record NewDeploymentCommand(Guid SiteId, string? Comment) : IRequest;

public class NewDeploymentCommandHandler : IRequestHandler<NewDeploymentCommand, Unit>
{
    private readonly IAzureDevopsRepositoryHelper _repositoryHelper;
    private readonly IAzureDevopsPipelineHelper _pipelineHelper;
    private readonly IApplicationDbContext _context;
    private readonly ElementsHelper _elementsHelper;
    private readonly DeploymentHelper _deploymentHelper;

    public NewDeploymentCommandHandler(IAzureDevopsRepositoryHelper repositoryHelper, IAzureDevopsPipelineHelper pipelineHelper, IApplicationDbContext context, ElementsHelper elementsHelper, DeploymentHelper deploymentHelper)
    {
        _repositoryHelper = repositoryHelper;
        _pipelineHelper = pipelineHelper;
        _context = context;
        _elementsHelper = elementsHelper;
        _deploymentHelper = deploymentHelper;
    }

    public async Task<Unit> Handle(NewDeploymentCommand request, CancellationToken cancellationToken)
    {
        var site = await _context.Sites.FirstOrDefaultAsync(x => x.Id == request.SiteId, cancellationToken) ?? throw new NotFoundException(nameof(Site), request.SiteId);

        await _deploymentHelper.DispachNewProgress(site.Id, 10, 1);

        var repository = await _repositoryHelper.GetOrCreateRepositoryAsync(site.Id.ToString());

        await _deploymentHelper.DispachNewProgress(site.Id, 25, 2);

        var pages = await _context.Pages
            .Where(x => x.Site.Id == request.SiteId)
            .ToListAsync(cancellationToken);

        var pagesDeployment = new List<PageDeployment>();

        foreach (var page in pages)
        {
            
            var elements = ElementDto.Projection(await _elementsHelper.MergeElementsForMenu(page.Id, cancellationToken));

            elements = await SetPageNameToLinkElement(elements);

            pagesDeployment.Add(PageDeployment.Projection(page, elements));
        }

        await _deploymentHelper.DispachNewProgress(site.Id, 30, 2);

        await _repositoryHelper.UploadFilesToRepo(repository, SiteDeployment.Projection(site), pagesDeployment, request.Comment);

        await _deploymentHelper.DispachNewProgress(site.Id, 50, 3);

        await _pipelineHelper.CreatePipeline(repository, site.Id.ToString());

        await _deploymentHelper.DispachNewProgress(site.Id, 75, 4);

        return Unit.Value;
    }

    private async Task<List<ElementDto>> SetPageNameToLinkElement(List<ElementDto> elements)
    {

        foreach(var element in elements)
        {
            if (element.Type != Domain.Enums.TypeElement.Link)
                continue;

            var page = await _context.Pages.FirstOrDefaultAsync(x => x.Id == element.PageId);

            if (page == null)
                new InvalidOperationException($"Not found page with ID: {element.PageId}");

            element.PageName = page.Name;

            if (element.ElementsChilds != null)
                element.ElementsChilds = await SetPageNameToLinkElement(element.ElementsChilds);
        }

        return elements;

    }
}
