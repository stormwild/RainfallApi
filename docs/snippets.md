```csharp
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

        // // Mock data - replace this with actual data retrieval logic
        // var mockReadings = new List<RainfallReading>
        // {
        //     new RainfallReading { DateMeasured = DateTime.Now.AddDays(-1), AmountMeasured = 5.2m },
        //     new RainfallReading { DateMeasured = DateTime.Now, AmountMeasured = 3.7m }
        // };

        // // Assuming Count is used to limit the number of readings returned
        // var limitedReadings = mockReadings.Take(req.Count).ToList();

        // // Construct and send the response
        // var response = new RainfallReadingsResponse { Readings = limitedReadings };
using FastEndpoints;

namespace RainfallApi;

public class ExampleEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/example");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync(new { message = "Hello, World!" });
        // return Results.Ok("Hello, World!");
    }

}

using System;

namespace MyCRMAPI.Core.Services;

public sealed class ResultOrError<TResult, TError>
{
    public bool IsError { get; }
    public TResult Result { get; }
    public TError Error { get; }

    public ResultOrError(TResult result) => Result = result;

    public ResultOrError(TError error)
    {
        Error = error;
        IsError = true;
    }

    public static implicit operator ResultOrError<TResult, TError>(TResult result) => new ResultOrError<TResult, TError>(result);
    public static implicit operator ResultOrError<TResult, TError>(TError error) => new ResultOrError<TResult, TError>(error);

    public TOutcome Match<TOutcome>(Func<TResult, TOutcome> success, Func<TError, TOutcome> failure) => IsError ? failure(Error) : success(Result);
}

namespace RainfallApi;

public sealed class Result<TResult, TError>
{
    public bool IsError { get; }
    public TResult Success { get; }
    public TError Error { get; }

    public Result(TResult result)
    {
        Success = result;
        IsError = false;
        Error = default;
    }

    public Result(TError error)
    {
        Success = default;
        Error = error;
        IsError = true;
    }

    public static implicit operator Result<TResult, TError>(TResult result) => new Result<TResult, TError>(result);
    public static implicit operator Result<TResult, TError>(TError error) => new Result<TResult, TError>(error);

    public TOutcome Match<TOutcome>(Func<TResult, TOutcome> success, Func<TError, TOutcome> failure) => IsError ? failure(Error) : success(Success);
}

```
