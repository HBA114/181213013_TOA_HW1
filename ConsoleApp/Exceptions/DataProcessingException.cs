namespace ConsoleApp.Exceptions;
public class DataProcessingException : Exception
{
    public DataProcessingException(string message) : base(message)
    {
    }
}