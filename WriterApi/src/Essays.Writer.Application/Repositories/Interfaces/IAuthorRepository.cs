using Essays.Core.Models.Models;

namespace Essays.Writer.Application.Repositories.Interfaces;

public interface IAuthorRepository
{
    /// <summary>
    /// Ensures that the specified list of authors exists in the data store.
    /// If an author does not exist, it will be added, and the updated list
    /// of authors will be returned.
    /// </summary>
    /// <param name="authors">A list of authors to ensure in the data store.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of authors with updated information, such as IDs.</returns>
    public Task<List<Author>> EnsureAuthors(List<Author> authors, CancellationToken cancellationToken = default);
}