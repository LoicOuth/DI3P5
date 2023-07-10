using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using USite.Application.Common.Interfaces;
using USite.Application.Common.Models;

namespace USite.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    private async Task<ApplicationUser> GetUserAsync(string userId)
    {
        return await _userManager.Users.FirstAsync(u => u.Id == userId);
    }

    public async Task<string> GetFirstNameAsync(string userId)
    {
        var user = await GetUserAsync(userId);

        return user.FirstName!;
    }

    public async Task<string> GetLastNameAsync(string userId)
    {
        var user = await GetUserAsync(userId);

        return user.LastName!;
    }

    public async Task<string> GetEmailAsync(string userId)
    {
        var user = await GetUserAsync(userId);

        return user.Email!;
    }

    public async Task<string> GetUsernameAsync(string userId)
    {
        var user = await GetUserAsync(userId);

        return user.UserName!;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await GetUserAsync(userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await GetUserAsync(userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await GetUserAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<Result> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
    {
        var user = await GetUserAsync(userId);

        //If user ask new password, and have a password (not a google account)
        if (!await _userManager.HasPasswordAsync(user)) throw new InvalidOperationException("User has no password");

        var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        return result.ToApplicationResult();
    }

    public async Task<Result> ChangePhoneNumberAsync(string userId, string newPhoneNumber)
    {
        var user = await GetUserAsync(userId);

        var result = await _userManager.SetPhoneNumberAsync(user, newPhoneNumber);

        return result.ToApplicationResult();
    }

    public async Task<Result> ChangeEmailAsync(string userId, string newEmail)
    {
        var user = await GetUserAsync(userId);

        var result = await _userManager.SetEmailAsync(user, newEmail);

        return result.ToApplicationResult();
    }

    public async Task<Result> ChangeUsernameAsync(string userId, string newUsername)
    {
        var user = await GetUserAsync(userId);

        var result = await _userManager.SetUserNameAsync(user, newUsername);

        return result.ToApplicationResult();
    }

    public async Task<Dictionary<string, string>> DownloadPersonalData(string userId)
    {
        var user = await GetUserAsync(userId);

        // Only include personal data for download
        var personalData = new Dictionary<string, string>();
        typeof(ApplicationUser).GetProperties()
            .Where(prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)))
            .ToList().ForEach(x => personalData.Add(x.Name, x.GetValue(user)?.ToString() ?? "null"));

        (await _userManager.GetLoginsAsync(user)).ToList()
            .ForEach(l => personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey));

        var key = await _userManager.GetAuthenticatorKeyAsync(user);
        if (!string.IsNullOrEmpty(key))
            personalData.Add($"Authenticator Key", key);

        return personalData;
    }
}
