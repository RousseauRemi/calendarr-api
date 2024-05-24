using calendarr_web_api.Application.InfrastructureInterfaces.WebRepository;

namespace calendarr_web_api.Infrastructure.WebRepository;


public class JellyseerExternalServiceRepository : IJellyseerExternalServiceRepository
{
    private readonly IHttpClientFactory _clientFactory;

    public JellyseerExternalServiceRepository(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<string?> GetAsync(string url,  string apikey, CancellationToken ct)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        
        var client = _clientFactory.CreateClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", apikey);
        using var response = await client.SendAsync(request, ct);
        
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync(ct);

        return content;
    }

    public async Task<bool> TestJellyseerAsync(string url, CancellationToken ct)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{url}/api/v1/status");
        
        var client = _clientFactory.CreateClient();
        
        using var response = await client.SendAsync(request, ct);

        return response.IsSuccessStatusCode;
    }
}