using Essays.Core.Models.Models;
using Essays.Retriever.Application.Repositories.Interfaces;
using Essays.Retriever.Application.Services.Interfaces;

namespace Essays.Retriever.Application.Services;

public class EssayRetrieverService(IEssayCacheService essayCacheService, IEssayRetrieverRepository essayRetrieverRepository) : IEssayRetrieverService
{
    /// <inherit/>
    public async Task<Essay?> GetEssay(Guid id, CancellationToken cancellationToken = default)
    {
        var essay = await essayCacheService.GetEssay(id);

        if (essay != null)
        {
            return essay;
        }

        essay = await essayRetrieverRepository.GetEssay(id, cancellationToken);

        if (essay != null)
        {
            await essayCacheService.CacheEssay(essay);
        }

        return essay;
    }
}