using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using USite.Application.Common.Interfaces;
using USite.Infrastructure.Settings;

namespace USite.Infrastructure.Email;

public class EmailSender : IEmailSender
{
    private const string FROM = "contact-usite@diiage.org";
    private const string SMTP_SERVER = "smtp.office365.com";
    private const int SMTP_PORT = 587;

    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<bool> SendAsync(string to, string subject, string messageText, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Send and email");
        // Création de l'objet MailMessage
        MailMessage message = new(FROM, to)
        {
            Subject = subject,
            Body = messageText
        };

        // Paramètres SMTP
        var smtpClient = new SmtpClient(SMTP_SERVER, SMTP_PORT)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(FROM, _configuration.GetOvhEmailPassword())
        };

        try
        {
            await smtpClient.SendMailAsync(message, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during sending email {ex}", ex);
            return false;
        }
        return true;
    }
}
