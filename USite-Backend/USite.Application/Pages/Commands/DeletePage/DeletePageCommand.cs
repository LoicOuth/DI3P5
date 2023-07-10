namespace USite.Application.Pages.Commands.DeletePage;

public record DeletePageCommand() : IRequest;

public class DeletePageCommandHandler : IRequestHandler<DeletePageCommand>
{
    public Task<Unit> Handle(DeletePageCommand request, CancellationToken cancellationToken)
    {
        //TODO
        throw new NotImplementedException();
    }
}
