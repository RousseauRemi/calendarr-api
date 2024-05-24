namespace calendarr_web_api.Domain;

public class JellyseerEntity
{
    public string Url { get; set; }
    public string Name { get; set; }
    public string ApiKey { get; set; }

    public JellyseerEntity(string name, string url, string apiKey)
    {
        Name = name;
        Url = url;
        ApiKey = apiKey;
    }
    
    public void Modified(JellyseerEntity other)
    {
        Url = other.Url;
        ApiKey = other.ApiKey;
    }
}