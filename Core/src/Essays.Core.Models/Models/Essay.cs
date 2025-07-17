namespace Essays.Core.Models.Models;

public record Essay(Guid Id, string Title, byte[] CompressedBody, string Author, DateTimeOffset CreatedAt);