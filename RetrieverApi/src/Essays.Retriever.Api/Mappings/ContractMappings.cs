using Essays.Core.Libraries.Extensions;
using Essays.Core.Models.Models;
using Essays.Retriever.Contracts.Responses;

namespace Essays.Retriever.Api.Mappings;

public static class ContractMappings
{
    public static EssayResponse MapToEssayResponse(this Essay essay) =>
        new(essay.Id, essay.Title, essay.CompressedBody.DecompressWithGzip(), essay.Author, essay.CreatedWhen);
}