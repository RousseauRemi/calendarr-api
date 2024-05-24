namespace calendarr_web_api.Application.UseCases.ArrApi;
public record ArrApiResult(string Url, bool IsReacheable, string? ResultContent);

public static class ArrApiExtension
{
    public static string DefaultContent = "[]";

}