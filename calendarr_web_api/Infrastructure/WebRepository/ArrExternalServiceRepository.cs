using calendarr_web_api.Application.InfrastructureInterfaces.WebRepository;

namespace calendarr_web_api.Infrastructure.WebRepository;


public class ArrExternalServiceRepository : IArrExternalServiceRepository
{
    private readonly IHttpClientFactory _clientFactory;

    public ArrExternalServiceRepository(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<string?> GetAsync(string url,  CancellationToken ct)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        
        var client = _clientFactory.CreateClient();
        
        using var response = await client.SendAsync(request, ct);
        
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync(ct);

        return content;
    }
    
    public async Task<byte[]?> GetImageAsync(string url,  CancellationToken ct)
    {
        // Validate the URL
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("URL cannot be null or whitespace.", nameof(url));
        }

        // Create the HTTP request message
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        // Create the HTTP client from the factory
        var client = _clientFactory.CreateClient();

        try
        {
            // Send the HTTP request and get the response
            var response = await client.SendAsync(request, ct);
            response.EnsureSuccessStatusCode();

            // Check the content type to make sure it's an image
            var contentType = response.Content.Headers.ContentType.MediaType;
            if (contentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
            {
                // Read the image bytes from the response content
                return await response.Content.ReadAsByteArrayAsync();
            }
            else
            {
                throw new InvalidOperationException("The response content is not an image.");
            }
        }
        catch (TaskCanceledException) when (ct.IsCancellationRequested)
        {
            // If the request was canceled, rethrow the exception
            throw new OperationCanceledException("The request was canceled.", ct);
        }
        catch (HttpRequestException ex)
        {
            // Log and rethrow the HttpRequestException
            // Replace the following line with your logging mechanism
            Console.WriteLine($"Request error: {ex.Message}");
            throw;
        }

        return null;
    }

    public async Task<bool> TestArrAsync(string url, string apiKey, CancellationToken ct)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{url}/ping?apikey={apiKey}");
        
        var client = _clientFactory.CreateClient();
        
        using var response = await client.SendAsync(request, ct);

        return response.IsSuccessStatusCode;
    }
}