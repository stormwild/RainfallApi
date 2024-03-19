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

```
