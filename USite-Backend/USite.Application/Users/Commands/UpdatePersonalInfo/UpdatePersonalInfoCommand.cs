namespace USite.Application.Users.Commands.UpdatePersonalInfo;

[Authorize]
public record UpdatePersonalInfoCommand(string NewUsername, string NewEmail) : IRequest;

public class UpdatePersonalInfoCommandHandler : IRequestHandler<UpdatePersonalInfoCommand>
{
    private readonly ILogger<UpdatePersonalInfoCommandHandler> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    public UpdatePersonalInfoCommandHandler(ILogger<UpdatePersonalInfoCommandHandler> logger, ICurrentUserService currentUserService, IIdentityService identityService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    public async Task<Unit> Handle(UpdatePersonalInfoCommand request, CancellationToken cancellationToken)
    {
        string userId = _currentUserService.UserId!;

        if (await _identityService.GetEmailAsync(userId) != request.NewEmail)
        {
            var result = await _identityService.ChangeEmailAsync(userId, request.NewEmail);

            if (!result.Succeeded)
            {
                _logger.LogError("Error during set new email for user {id}", userId);
                throw new InvalidOperationException("Error during set new email");
            }
        }

        if (await _identityService.GetUsernameAsync(userId) != request.NewUsername)
        {
            var result = await _identityService.ChangeUsernameAsync(userId, request.NewUsername);

            if (!result.Succeeded)
            {
                _logger.LogError("Error during set new username for user {id}", userId);
                throw new InvalidOperationException("Error during set new username");
            }
        }

        return Unit.Value;
    }
}
