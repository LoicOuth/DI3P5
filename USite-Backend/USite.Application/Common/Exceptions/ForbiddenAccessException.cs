namespace USite.Application.Common.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base() { }

    public ForbiddenAccessException(string name, string key) : base($"Forbidden Access for Entity \"{name}\" ({key}).") { }
}