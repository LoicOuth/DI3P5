namespace USite.Application.Users.Commands.UpdatePassword;

public record UpdatePasswordCommand(string OldPassword, string NewPassword) : IRequest;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<UpdatePasswordCommandHandler> _logger;

    public UpdatePasswordCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService, ILogger<UpdatePasswordCommandHandler> logger)
    {
        _identityService = identityService;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        string userId = _currentUserService.UserId!;

        var result = await _identityService.ChangePasswordAsync(userId, request.OldPassword, request.NewPassword);

        if (!result.Succeeded)
        {
            _logger.LogError("Error during set new last name for user {id}", userId);
            throw new InvalidOperationException($"Error during set new last name");
        }

        return Unit.Value;
    }
}
