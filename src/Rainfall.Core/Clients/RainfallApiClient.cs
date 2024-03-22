using Rainfall.Core.Common;
using System.Text.Json;

namespace Rainfall.Core.Clients;


public interface IRainfallApiClient
{
    public Task<Result<ReadingResponse, RainfallApiClientError>> GetStationReadingsAsync(string stationId, int limit, CancellationToken ct);
}

public class RainfallApiClient(HttpClient httpClient) : IRainfallApiClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Result<ReadingResponse, RainfallApiClientError>> GetStationReadingsAsync(string stationId, int limit, CancellationToken ct)
    {
        var response = await _httpClient.GetAsync($"id/stations/{stationId}/readings?_sorted&_limit={limit}", ct);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync(ct);
        var result = JsonSerializer.Deserialize<ReadingResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ReadingResponse();

        if (result is null || result.Items.Count == 0)
        {
            return RainfallApiClientError.EmptyItems;
        }

        return result;
    }
}

public enum RainfallApiClientError
{
    EmptyItems
}