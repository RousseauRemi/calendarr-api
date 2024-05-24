namespace calendarr_web_api.Application.UseCases.Jellyseer;
public record JellyseerApiResult(string Url ,bool IsReacheable, string? ResultContent);

public static class JellyseerApiExtension
{
    public static string DefaultContent = "[]";

}