namespace USite.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string> GetFirstNameAsync(string userId);

    Task<string> GetLastNameAsync(string userId);

    Task<string> GetEmailAsync(string userId);

    Task<string> GetUsernameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);
    
    Task<Result> DeleteUserAsync(string userId);

    Task<Result> ChangePasswordAsync(string userId, string oldPassword, string newPassword);

    Task<Result> ChangePhoneNumberAsync(string userId, string newPhoneNumber);

    Task<Result> ChangeEmailAsync(string userId, string newEmail);

    Task<Result> ChangeUsernameAsync(string userId, string newUsername);

    Task<Dictionary<string, string>> DownloadPersonalData(string userId);
}