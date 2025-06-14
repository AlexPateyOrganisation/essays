namespace Essays.Retriever.Contracts.Responses;

public record EssayResponse(Guid Id, string Title, string Body, string Author, DateTime CreatedAt);