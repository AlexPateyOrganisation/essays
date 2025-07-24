namespace Essays.Writer.Contracts.Responses;

public record EssayResponse(Guid Id, string Title, string Body, List<AuthorResponse> Authors, DateTimeOffset CreatedWhen);