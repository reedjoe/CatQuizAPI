namespace CatQuiz.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
        : base()
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string type, string key, string value)
        : base($"Entity of type '{type}' with property '{key}' equal to '{value}' was not found.")
    {
    }

    public NotFoundException(string type, string key, List<string> values)
        : base($"Entity of type '{type}' with property '{key}' in list '{string.Join(", ", values)}' was not found.")
    {
    }
}
