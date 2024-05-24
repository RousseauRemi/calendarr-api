namespace calendarr_web_api.Infrastructure.Exceptions;

public class CustomDataException : System.Data.DataException
{
    public CustomDataException(string message) : base(message)
    {
        
    }
}