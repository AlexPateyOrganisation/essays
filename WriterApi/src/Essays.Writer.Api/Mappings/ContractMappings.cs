using Essays.Core.Libraries.Extensions;
using Essays.Core.Models.Models;
using Essays.Writer.Contracts.Requests;
using Essays.Writer.Contracts.Responses;

namespace Essays.Writer.Api.Mappings;

public static class ContractMappings
{
    public static Essay MapToEssay(this EssayRequest essayRequest) =>
        new(Guid.NewGuid(), essayRequest.Title, essayRequest.Body.CompressWithGzip(), essayRequest.Author, DateTimeOffset.Now);

    public static EssayResponse MapToEssayResponse(this Essay essay) =>
        new(essay.Id, essay.Title, essay.CompressedBody.DecompressWithGzip(), essay.Author, essay.CreatedAt);
}