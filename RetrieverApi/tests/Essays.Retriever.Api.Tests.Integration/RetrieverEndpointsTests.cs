using System.Net;
using System.Net.Http.Json;
using Essays.Retriever.Contracts.Responses;
using Shouldly;

namespace Essays.Retriever.Api.Tests.Integration;

public class RetrieverEndpointsTests(ApiFixture fixture) : IClassFixture<ApiFixture>
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
        responseContent.ShouldBe("Retriever API");
    }

    [Fact]
    public async Task GetEssayEndpoint_ShouldReturnOkResponse_WhenCalledWithExistingEssayId()
    {
        //Arrange
        Guid essayId = new("f76a1529-1725-469d-8d27-8fb0f0e61c40");

        //Act
        var response = await _client.GetAsync($"/essays/{essayId}");
        var responseContent = await response.Content.ReadFromJsonAsync<EssayResponse>();

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        responseContent.ShouldNotBeNull();
        responseContent.Title.ShouldBe("Test Title");
        responseContent.Authors.ShouldNotBeEmpty();
        responseContent.Authors.First().FirstName.ShouldBe("Author First Name");
        responseContent.Authors.First().LastName.ShouldBe("Author Last Name");
        responseContent.Body.ShouldBe("This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement.");
    }

    [Fact]
    public async Task GetEssayEndpoint_ShouldReturnNotFoundResponse_WhenCalledWithNotExistingEssayId()
    {
        //Arrange
        Guid essayId = new("a8494ddd-41f7-452c-bab7-8ac18bcd3a7d");

        //Act
        var response = await _client.GetAsync($"/essays/{essayId}");

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}