namespace USite.Application.Users.Queries.DownloadPersonalData;


[Authorize]
public record DownloadPersonalDataCommand() : IRequest<Dictionary<string, string>>;

public class DownloadPersonalDataCommandHandler : IRequestHandler<DownloadPersonalDataCommand, Dictionary<string, string>>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<DownloadPersonalDataCommandHandler> _logger;

    public DownloadPersonalDataCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService, ILogger<DownloadPersonalDataCommandHandler> logger)
    {
        _identityService = identityService;
        _currentUserService = currentUserService;
        _logger = logger;
    }
    public async Task<Dictionary<string, string>> Handle(DownloadPersonalDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _currentUserService.UserId);

        return await _identityService.DownloadPersonalData(_currentUserService.UserId!);
    }
}