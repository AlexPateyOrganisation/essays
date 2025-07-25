using Essays.Core.Models.Models;

namespace Essays.Retriever.Application.Services.Interfaces;

public interface IEssayCacheService
{
    /// <summary>
    /// Retrieves an essay by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the essay.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved essay, or null if no essay is found.</returns>
    public Task<Essay?> GetEssay(Guid id);

    /// <summary>
    /// Caches the specified essay.
    /// </summary>
    /// <param name="essay">The essay to be cached. If null, no action is performed.</param>
    /// <returns>A task that represents the asynchronous caching operation.</returns>
    public Task CacheEssay(Essay? essay);
}