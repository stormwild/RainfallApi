using System.Text.Json;

namespace RainfallApi.Clients;


public interface IRainfallApiClient
{
    public Task<ReadingResponse> GetStationReadingsAsync(string stationId, int limit, CancellationToken ct);
}

public class RainfallApiClient : IRainfallApiClient
{
    private readonly HttpClient _httpClient;

    public RainfallApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ReadingResponse> GetStationReadingsAsync(string stationId, int limit, CancellationToken ct)
    {
        var response = await _httpClient.GetAsync($"id/stations/{stationId}/readings?_sorted&_limit={limit}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync(ct);
        return JsonSerializer.Deserialize<ReadingResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ReadingResponse();
    }
}