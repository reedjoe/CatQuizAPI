namespace CatQuiz.Core.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException()
        : base("An invalid request was provided.")
    {
    }

    public BadRequestException(string message)
        : base(message)
    {
    }
}
