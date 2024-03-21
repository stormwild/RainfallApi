using FluentAssertions;
using NSubstitute;
using Rainfall.Core.Clients;
using System.Net;
using System.Text;

namespace Rainfall.Api.Tests;

public class RainfallApiClientTests
{
    [Fact]
    public async Task Returns_ReadingResponse_When_Valid_Response_Received()
    {
        // Arrange
        var client = new HttpClient(new DelegatingHandlerStub())
        {
            BaseAddress = new Uri("https://environment.data.gov.uk/flood-monitoring/") // Replace with your actual API base URL
        };

        // Act
        var service = new RainfallApiClient(client); // Pass the HttpClient instance to the constructor
        ReadingResponse result = await service.GetStationReadingsAsync("3680", 100, CancellationToken.None); // Replace with your actual method

        // Assert
        // Add your assertions here
        result.Should().NotBeNull();
        result.Should().BeOfType<ReadingResponse>();
        result.Items.Should().NotBeEmpty();
        result.Items.Should().BeOfType<List<Item>>();
    }
}

public class DelegatingHandlerStub : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var json = File.ReadAllText("response.json"); // Path to your JSON file
        return await Task.FromResult(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        });
    }
}
