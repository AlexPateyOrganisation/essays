using Essays.Core.Models.Models;

namespace Essays.Retriever.Application.Repositories.Interfaces;

public interface IEssayRetrieverRepository
{
    /// <summary>
    /// Retrieves an essay by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the essay to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the essay if found, otherwise null.</returns>
    public Task<Essay?> GetEssay(Guid id, CancellationToken cancellationToken = default);
}