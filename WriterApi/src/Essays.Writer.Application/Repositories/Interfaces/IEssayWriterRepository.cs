using Essays.Core.Models.Models;

namespace Essays.Writer.Application.Repositories.Interfaces;

public interface IEssayWriterRepository
{
    /// <summary>
    /// Creates a new essay in the repository.
    /// </summary>
    /// <param name="essay">The essay object to be created.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created essay, or null if the creation was unsuccessful.</returns>
    public Task<Essay?> CreateEssay(Essay essay, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing essay in the repository.
    /// </summary>
    /// <param name="essay">The essay object containing updated information.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated essay, or null if the update was unsuccessful.</returns>
    public Task<Essay?> UpdateEssay(Essay essay, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an essay from the repository based on the given identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the essay to be deleted.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
    public Task<bool> DeleteEssay(Guid id, CancellationToken cancellationToken = default);
}