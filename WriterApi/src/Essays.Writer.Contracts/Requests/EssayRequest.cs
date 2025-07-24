namespace Essays.Writer.Contracts.Requests;

public record EssayRequest(string Title, string Body, List<AuthorRequest> Authors);