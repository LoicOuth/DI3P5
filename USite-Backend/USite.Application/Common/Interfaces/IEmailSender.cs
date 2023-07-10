namespace USite.Application.Common.Interfaces;

public interface IEmailSender
{
    Task<bool> SendAsync(string to, string subject, string messageText, CancellationToken cancellationToken);
}
