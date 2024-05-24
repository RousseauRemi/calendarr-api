using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace calendarr_web_api.Domain;

public class ApiEntity
{
    public string Url { get; set; }
    public string Name { get; set; }
    public string ApiKey { get; set; }
    public ApiTypeEnum ApiType { get; set; }
    public long Color { get; set; }

    public ApiEntity(string name, string url, string apiKey, ApiTypeEnum apiType, long color = 4294638330)
    {
        Name = name;
        Url = url;
        ApiKey = apiKey;
        ApiType = apiType;
        Color = color;
    }
    
    public void Modified(ApiEntity other)
    {
        Name = other.Name;
        Url = other.Url;
        ApiKey = other.ApiKey;
        ApiType = other.ApiType;
        Color = other.Color;
    }
}
public enum ApiTypeEnum
{
    Sonarr=0,
    Radarr=1,
    Lidarr=2,
    Readarr=3,
}