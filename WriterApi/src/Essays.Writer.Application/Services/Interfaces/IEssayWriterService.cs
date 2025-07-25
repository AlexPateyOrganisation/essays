using Essays.Core.Models.Models;

namespace Essays.Writer.Application.Services.Interfaces;

public interface IEssayWriterService
{
    /// <summary>
    /// Creates a new essay with the specified details, including its authors, and returns the created essay object.
    /// </summary>
    /// <param name="essay">The essay to be created, containing its details such as title, body, and authors.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The created <see cref="Essay"/> object if the operation is successful; otherwise, null.</returns>
    public Task<Essay?> CreateEssay(Essay essay, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the details of an existing essay and returns the updated essay object.
    /// </summary>
    /// <param name="essay">The essay with updated details, including its title, body, and authors.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The updated <see cref="Essay"/> object if the operation is successful; otherwise, null.</returns>
    public Task<Essay?> UpdateEssay(Essay essay, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an essay with the specified ID and returns a boolean indicating the success of the operation.
    /// </summary>
    /// <param name="id">The unique identifier of the essay to be deleted.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>True if the essay was successfully deleted; otherwise, false.</returns>
    public Task<bool> DeleteEssay(Guid id, CancellationToken cancellationToken = default);
}