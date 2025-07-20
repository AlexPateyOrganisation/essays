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
        Author = essayRequest.Author
    };

    public static EssayResponse MapToEssayResponse(this Essay essay) =>
        new(essay.Id, essay.Title, essay.CompressedBody.DecompressWithGzip(), essay.Author, essay.CreatedWhen);
}