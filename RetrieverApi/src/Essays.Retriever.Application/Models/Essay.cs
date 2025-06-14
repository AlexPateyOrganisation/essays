namespace Essays.Retriever.Application.Models;

public record Essay(Guid Id, string Title, byte[] CompressedBody, string Author, DateTime CreatedAt);