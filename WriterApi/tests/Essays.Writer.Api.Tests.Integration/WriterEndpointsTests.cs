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
            = new("Test Title", "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement.",
                [new AuthorRequest("Author First Name", "Author Last Name", new DateOnly(2000, 01, 01))]);

        //Act
        var response = await _client.PostAsJsonAsync("/essays", createEssayRequest);
        var responseContent = await response.Content.ReadFromJsonAsync<EssayResponse>();

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        responseContent.ShouldNotBeNull();
        responseContent.Title.ShouldBe("Test Title");
        responseContent.Authors.ShouldNotBeEmpty();
        responseContent.Authors.First().FirstName.ShouldBe("Author First Name");
        responseContent.Authors.First().LastName.ShouldBe("Author Last Name");
        responseContent.Body.ShouldBe("This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement.");
    }

    [Fact]
    public async Task CreateEssayEndpoint_ShouldReturnBadRequestResponse_WhenCalledWithEmptyTitle()
    {
        //Arrange
        EssayRequest createEssayRequest = new(string.Empty, "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement.",
            [new AuthorRequest("Author First Name", "Author Last Name", new DateOnly(2000, 01, 01))]);

        //Act
        var response = await _client.PostAsJsonAsync("/essays", createEssayRequest);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateEssayEndpoint_ShouldReturnBadRequestResponse_WhenCalledWithEmptyBody()
    {
        //Arrange
        EssayRequest createEssayRequest = new("Test Title", string.Empty, [new AuthorRequest("Author First Name", "Author Last Name", new DateOnly(2000, 01, 01))]);

        //Act
        var response = await _client.PostAsJsonAsync("/essays", createEssayRequest);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateEssayEndpoint_ShouldReturnBadRequestResponse_WhenCalledWithEmptyListOfAuthors()
    {
        //Arrange
        EssayRequest createEssayRequest = new("Test Title",
            "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement.", []);

        //Act
        var response = await _client.PostAsJsonAsync("/essays", createEssayRequest);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateEssayEndpoint_ShouldReturnOkResponse_WhenEssayIsUpdated()
    {
        //Arrange
        var essayId = new Guid("f76a1529-1725-469d-8d27-8fb0f0e61c40");
        EssayRequest updateEssayRequest = new("Updated Test Title", "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement.",
            [new AuthorRequest("Updated Author First Name", "Updated Author Last Name", new DateOnly(2000, 01, 01))]);

        //Act
        var response = await _client.PutAsJsonAsync($"/essays/{essayId}", updateEssayRequest);
        var responseContent = await response.Content.ReadFromJsonAsync<EssayResponse>();

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        responseContent.ShouldNotBeNull();
        responseContent.Title.ShouldBe("Updated Test Title");
        responseContent.Authors.ShouldNotBeEmpty();
        responseContent.Authors.First().FirstName.ShouldBe("Updated Author First Name");
        responseContent.Authors.First().LastName.ShouldBe("Updated Author Last Name");
        responseContent.Body.ShouldBe("This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement.");
    }

    [Fact]
    public async Task UpdateEssayEndpoint_ShouldReturnNotFoundResponse_WhenEssayNotFound()
    {
        //Arrange
        var essayId = new Guid("a8494ddd-41f7-452c-bab7-8ac18bcd3a7d");
        EssayRequest updateEssayRequest = new("Test Title", "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement.",
            [new AuthorRequest("Author First Name", "Author Last Name", new DateOnly(2000, 01, 01))]);

        //Act
        var response = await _client.PutAsJsonAsync($"/essays/{essayId}", updateEssayRequest);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateEssayEndpoint_ShouldReturnBadRequestResponse_WhenCalledWithEmptyTitle()
    {
        //Arrange
        var essayId = new Guid("a8494ddd-41f7-452c-bab7-8ac18bcd3a7d");
        EssayRequest updateEssayRequest = new(string.Empty, "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement.",
            [new AuthorRequest("Author First Name", "Author Last Name", new DateOnly(2000, 01, 01))]);

        //Act
        var response = await _client.PutAsJsonAsync($"/essays/{essayId}", updateEssayRequest);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateEssayEndpoint_ShouldReturnBadRequestResponse_WhenCalledWithEmptyBody()
    {
        //Arrange
        var essayId = new Guid("a8494ddd-41f7-452c-bab7-8ac18bcd3a7d");
        EssayRequest updateEssayRequest = new("Test Title", string.Empty,
            [new AuthorRequest("Author First Name", "Author Last Name", new DateOnly(2000, 01, 01))]);

        //Act
        var response = await _client.PutAsJsonAsync($"/essays/{essayId}", updateEssayRequest);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateEssayEndpoint_ShouldReturnBadRequestResponse_WhenCalledWithEmptyListOfAuthors()
    {
        //Arrange
        var essayId = new Guid("a8494ddd-41f7-452c-bab7-8ac18bcd3a7d");
        EssayRequest updateEssayRequest = new("Test Title", "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement.", []);

        //Act
        var response = await _client.PutAsJsonAsync($"/essays/{essayId}", updateEssayRequest);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteEssayEndpoint_ShouldReturnOkResponse_WhenEssayIsDeleted()
    {
        //Arrange
        var essayId = new Guid("123a1529-1725-469d-8d27-8fb0f0e61c40");

        //Act
        var response = await _client.DeleteAsync($"/essays/{essayId}");

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteEssayEndpoint_ShouldReturnNotFoundResponse_WhenEssayNotFound()
    {
        //Arrange
        var essayId = new Guid("a8494123-41f7-452c-bab7-8ac18bcd3a7d");

        //Act
        var response = await _client.DeleteAsync($"/essays/{essayId}");

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}