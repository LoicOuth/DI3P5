namespace USite.Application.Users.Dto;
public class UserInfoDto
{
    public string Email { get; set; }
    public string Username { get; set; }
    public UserInfoDto(string email, string username)
    {
        Email = email;
        Username = username;
    }
}
