namespace calendarr_web_api.Application.InfrastructureInterfaces.WebRepository;

public interface IJellyseerExternalServiceRepository
{
    public Task<string?> GetAsync(string url,  string apikey, CancellationToken ct);
    public Task<bool> TestJellyseerAsync(string url, CancellationToken ct);
}