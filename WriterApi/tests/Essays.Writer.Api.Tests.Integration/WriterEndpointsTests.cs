using System.Net;
using System.Net.Http.Json;
using Essays.Writer.Contracts.Requests;
using Essays.Writer.Contracts.Responses;
using Shouldly;

namespace Essays.Writer.Api.Tests.Integration;

public class WriterEndpointsTests(ApiFixture fixture) : IClassFixture<ApiFixture>
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task GetEndpoint_ShouldReturnOkResponseAndApiName_WhenCalled()
    {
        //Act
        var response = await _client.GetAsync("/");
        var responseContent = await response.Content.ReadAsStringAsync();

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        responseContent.ShouldBe("Writer API");
    }

    [Fact]
    public async Task CreateEssayEndpoint_ShouldReturnCreatedResponse_WhenCalledWithValidRequest()
    {
        //Arrange
        EssayRequest createEssayRequest
            = new("Test Title", "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement.", "Test Author");

        //Act
        var response = await _client.PostAsJsonAsync("/essays", createEssayRequest);
        var responseContent = await response.Content.ReadFromJsonAsync<EssayResponse>();

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        responseContent.ShouldNotBeNull();
        responseContent.Title.ShouldBe("Test Title");
        responseContent.Author.ShouldBe("Test Author");
        responseContent.Body.ShouldBe("This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement.");
    }

    [Fact]
    public async Task CreateEssayEndpoint_ShouldReturnBadRequestResponse_WhenCalledWithInvalidRequest()
    {
        //Arrange
        EssayRequest createEssayRequest = new(string.Empty, string.Empty, string.Empty);

        //Act
        var response = await _client.PostAsJsonAsync("/essays", createEssayRequest);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}