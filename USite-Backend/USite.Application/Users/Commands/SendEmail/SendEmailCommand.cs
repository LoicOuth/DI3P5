namespace USite.Application.Users.Commands.SendEmail;
public record SendEmailCommand(string To, string Subject, string Message): IRequest;

public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand>
{
    private readonly IEmailSender _emailSender;
    public SendEmailCommandHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
    public async Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        await _emailSender.SendAsync(request.To, request.Subject, request.Message, cancellationToken);
        return Unit.Value;
    }
}
