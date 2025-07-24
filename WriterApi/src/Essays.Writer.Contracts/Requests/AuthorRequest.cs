namespace Essays.Writer.Contracts.Requests;

public record AuthorRequest(string FirstName, string LastName, DateOnly DateOfBirth);