using USite.Application.Users.Dto;

namespace USite.Application.Users.Commands.GetUserInfo;

[Authorize]
public record GetUserInfoCommand (): IRequest<UserInfoDto>;

public class GetUserInfoCommandHandler : IRequestHandler<GetUserInfoCommand, UserInfoDto>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public GetUserInfoCommandHandler(ICurrentUserService currentUserService, IIdentityService identityService)
    {
        _currentUserService = currentUserService;
        _identityService = identityService;
    }
    public async Task<UserInfoDto> Handle(GetUserInfoCommand request, CancellationToken cancellationToken)
    {
        string userId = _currentUserService.UserId!;

        var username = await _identityService.GetUsernameAsync(userId);

        var email = await _identityService.GetEmailAsync(userId);

        return new UserInfoDto(email, username);
    }
}