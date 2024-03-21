using FastEndpoints.Testing;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Rainfall.Api.Endpoints.Rainfall;
using System.Net;
using System.Net.Http.Json;

namespace Rainfall.Api.Tests;

public class App : AppFixture<Program>
{


}

public class RainfallApiTests(App app) : TestBase<App>
{
    [Fact]
    public async void Rainfall_Api_Returns_ValidResponse_When_Called()
    {
        var res = await app.Client.GetFromJsonAsync<RainfallReadingsResponse>("/rainfall/id/3680/readings?count=2");

        res.Should().BeOfType<RainfallReadingsResponse>();
    }
}
