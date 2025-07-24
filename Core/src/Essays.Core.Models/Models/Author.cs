using System.Text.Json.Serialization;

namespace Essays.Core.Models.Models;

public class Author
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required DateOnly DateOfBirth { get; init; }
    public required string Slug { get; init; }
    [JsonIgnore]
    public List<Essay> Essays { get; init; } = [];
}