namespace DbFirst.Exceptions;

public class NoBedsAvailableException : Exception
{
    public NoBedsAvailableException()
    {
    }

    public NoBedsAvailableException(string? message) : base(message)
    {
    }

    public NoBedsAvailableException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}