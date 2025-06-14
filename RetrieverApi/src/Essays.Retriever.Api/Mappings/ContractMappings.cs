using Essays.CoreLibraries.Extensions;
using Essays.Retriever.Application.Models;
using Essays.Retriever.Contracts.Responses;

namespace Essays.Retriever.Api.Mappings;

public static class ContractMappings
{
    public static EssayResponse MapToEssayResponse(this Essay essay) =>
        new(essay.Id, essay.Title, essay.CompressedBody.DecompressWithGzip(), essay.Author, essay.CreatedAt);
}