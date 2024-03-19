using Microsoft.AspNetCore.Mvc;

namespace RainfallApi.Endpoints.Rainfall;

// Define the request and response DTOs according to the OpenAPI spec
public class RainfallReadingsRequest
{
    [FromRoute(Name = "StationId")]
    public string StationId { get; set; } = string.Empty;
    [FromQuery(Name = "count")]
    public int? Count { get; set; } = RainfallExtensions.DefaultCount; // Default value as per spec
}

public class RainfallReading
{
    public DateTime DateMeasured { get; set; }
    public decimal AmountMeasured { get; set; }
}

public class RainfallReadingsResponse
{
    public List<RainfallReading> Readings { get; set; } = new();
}
