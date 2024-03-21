using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Rainfall.Core.Clients;

namespace Rainfall.Api.Endpoints.Rainfall;



public class RainfallEndpoint : Endpoint<RainfallReadingsRequest, Results<Ok<RainfallReadingsResponse>, BadRequest, NotFound>>
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
        Description(b => b
            .ProducesProblemFE<ProblemDetails>(400)
            .ProducesProblemFE<ProblemDetails>(404)
            .ProducesProblemFE<ProblemDetails>(500)
        );
        Summary(s =>
        {
            s.Summary = "Operations relating to rainfall";
            s.Description = "Get the rainfall readings for a given station";
        });

    }

    public override async Task<Results<Ok<RainfallReadingsResponse>, BadRequest, NotFound>> ExecuteAsync(RainfallReadingsRequest req, CancellationToken ct)
    {
        var response = await _client.GetStationReadingsAsync(req.StationId, req.Count ?? RainfallExtensions.DefaultCount, ct);
        return TypedResults.Ok(new RainfallReadingsResponse
        {
            Readings = response.Items.Select(item => item.ToRainfallReading()).ToList()
        });
    }
}

public static class RainfallExtensions
{
    public const int DefaultCount = 10;
    public static RainfallReading ToRainfallReading(this Item item)
    {
        return new RainfallReading
        {
            DateMeasured = DateTimeOffset.Parse(item.DateTime),
            AmountMeasured = Convert.ToDecimal(item.Value)
        };
    }
}