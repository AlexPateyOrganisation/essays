namespace Essays.Writer.Contracts.Responses;

public record AuthorResponse(Guid Id, string FirstName, string LastName, DateOnly DateOfBirth, string Slug);