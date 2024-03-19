using FastEndpoints;
using RainfallApi.Clients;
using RainfallApi.Endpoints.Rainfall;
using System.Reflection.Metadata;

namespace RainfallApi;



public class RainfallEndpoint : Endpoint<RainfallReadingsRequest, RainfallReadingsResponse>
{
    private readonly IRainfallApiClient _client;

    public RainfallEndpoint(IRainfallApiClient client)
    {
        _client = client;
    }

    public override void Configure()
    {
        Get("/rainfall/id/{StationId}/readings");
        Tags("Rainfall"); // Tagging as per OpenAPI spec
        AllowAnonymous();
    }

    public override async Task HandleAsync(RainfallReadingsRequest req, CancellationToken ct)
    {
        var response = await _client.GetStationReadingsAsync(req.StationId, req.Count ?? RainfallExtensions.DefaultCount, ct);
        await SendAsync(new RainfallReadingsResponse
        {
            Readings = response.Items.Select(item => item.ToRainfallReading()).ToList()
        }, cancellation: ct);
    }
}

public static class RainfallExtensions
{
    public const int DefaultCount = 10;
    public static RainfallReading ToRainfallReading(this Item item)
    {
        return new RainfallReading
        {
            DateMeasured = DateTime.Parse(item.DateTime),
            AmountMeasured = Convert.ToDecimal(item.Value)
        };
    }
}