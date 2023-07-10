using Microsoft.AspNetCore.Identity;

namespace USite.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public ApplicationUser(string firstName, string lastName):base(string.Format("{0}_{1}", firstName, lastName))
    {
        FirstName = firstName;
        LastName = lastName;
    }
    public ApplicationUser(string firstName, string lastName, string userName) : base(userName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}