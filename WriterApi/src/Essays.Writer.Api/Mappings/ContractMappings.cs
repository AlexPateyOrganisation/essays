using Essays.Core.Libraries.Extensions;
using Essays.Core.Models.Models;
using Essays.Writer.Contracts.Requests;
using Essays.Writer.Contracts.Responses;

namespace Essays.Writer.Api.Mappings;

public static class ContractMappings
{
    public static Essay MapToEssay(this EssayRequest essayRequest, Guid? id = null) => new()
    {
        Id = id ?? Guid.NewGuid(),
        Title = essayRequest.Title,
        CompressedBody = essayRequest.Body.CompressWithGzip(),
        Authors = essayRequest.Authors.Select(a => a.MapToAuthor()).ToList()
    };

    public static EssayResponse MapToEssayResponse(this Essay essay) =>
        new(essay.Id, essay.Title, essay.CompressedBody.DecompressWithGzip(), essay.Authors.Select(a => a.MapToAuthorResponse()).ToList(), essay.CreatedWhen);

    private static Author MapToAuthor(this AuthorRequest authorRequest, Guid? id = null) => new()
    {
        Id = id ?? Guid.NewGuid(),
        FirstName = authorRequest.FirstName,
        LastName = authorRequest.LastName,
        DateOfBirth = authorRequest.DateOfBirth,
        Slug = $"{authorRequest.FirstName}-{authorRequest.LastName}-{authorRequest.DateOfBirth:yyyyMMdd}"
    };

    private static AuthorResponse MapToAuthorResponse(this Author author) =>
        new(author.Id, author.FirstName, author.LastName, author.DateOfBirth, author.Slug);
}