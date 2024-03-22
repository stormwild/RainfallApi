using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Rainfall.Core.Clients;

namespace Rainfall.Api.Endpoints.Rainfall;



public class RainfallEndpoint(IRainfallApiClient client) : Endpoint<RainfallReadingsRequest, RainfallReadingsResponse>
{
    private readonly IRainfallApiClient _client = client;

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

    public override async Task<RainfallReadingsResponse> ExecuteAsync(RainfallReadingsRequest req, CancellationToken ct)
    {
        var results = await _client.GetStationReadingsAsync(req.StationId, req.Count ?? RainfallExtensions.DefaultCount, ct);
        return new RainfallReadingsResponse
        {
            Readings = results.Value.Items.Select(i => i.ToRainfallReading()).ToList()
        };

        // if (results is not null && results.IsError)
        // {
        //     return NotFound("No readings found for the given station");
        // }

        // return TypedResults.Ok Ok();

        // return  switch
        // {
        //     false => TypedResults.NotFound("No readings found for the given station"),
        //     true => TypedResults.Ok(new RainfallReadingsResponse { Readings = results.Value.Items.Select(i => i.ToRainfallReading()).ToList() })
        // };


        // return results.Match(
        //     success: r => TypedResults.Ok(new RainfallReadingsResponse { Readings = r.Items.Select(i => i.ToRainfallReading()).ToList() }) as Results<Ok<RainfallReadingsResponse>, BadRequest, NotFound>,
        //     failure: (error) =>
        //     {
        //         return error switch
        //         {
        //             RainfallApiClientError.EmptyItems => TypedResults.NotFound("No readings found for the given station") as Results<Ok<RainfallReadingsResponse>, BadRequest, NotFound>,
        //             _ => TypedResults.BadRequest("An error occurred while fetching the readings") as Results<Ok<RainfallReadingsResponse>, BadRequest, NotFound>,
        //         };
        //     }
        // )  as Results<Ok<RainfallReadingsResponse>, BadRequest, NotFound>;
    }

    // private Results<BadRequest, NotFound> MapError(RainfallApiClientError 
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