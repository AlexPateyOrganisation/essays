namespace Essays.Core.Models.Models;

public class Essay
{
    public required Guid Id { get; init; }
    public required string Title { get; set; }
    public required byte[] CompressedBody { get; set; }
    public DateTimeOffset CreatedWhen { get; init; }
    public List<Author> Authors { get; set; } = [];
}