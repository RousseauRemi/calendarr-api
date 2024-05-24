namespace calendarr_web_api.Application.InfrastructureInterfaces.WebRepository;

public interface IArrExternalServiceRepository
{
    public Task<string?> GetAsync(string url, CancellationToken ct);
    public Task<bool> TestArrAsync(string url, string apiKey,CancellationToken ct);
    Task<byte[]?> GetImageAsync(string url, CancellationToken ct);
}