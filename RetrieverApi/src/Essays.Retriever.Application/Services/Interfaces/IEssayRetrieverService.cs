using Essays.Core.Models.Models;

namespace Essays.Retriever.Application.Services.Interfaces;

public interface IEssayRetrieverService
{
    /// <summary>
    /// Retrieves an essay by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the essay.</param>
    /// <param name="cancellationToken">A token for canceling the operation.</param>
    /// <returns>The essay if found; otherwise, null.</returns>
    public Task<Essay?> GetEssay(Guid id, CancellationToken cancellationToken = default);
}