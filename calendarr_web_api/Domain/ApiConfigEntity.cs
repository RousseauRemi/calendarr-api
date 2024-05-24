namespace calendarr_web_api.Domain;

public class ApiConfigEntity
{
    public ApiConfigEntity(string url, bool configFromApiEnabled, bool configAndDataFromApiEnabled)
    {
        Url = url;
        ConfigAndDataFromApiEnabled = configAndDataFromApiEnabled;
        ConfigFromApiEnabled = configFromApiEnabled;
    }
    public string Url { get; set; }
    public bool ConfigFromApiEnabled { get; set; }
    public bool ConfigAndDataFromApiEnabled { get; set; }
    
    public void Modified(ApiConfigEntity other)
    {
        ConfigAndDataFromApiEnabled = other.ConfigAndDataFromApiEnabled;
        ConfigFromApiEnabled = other.ConfigFromApiEnabled;
    }
}