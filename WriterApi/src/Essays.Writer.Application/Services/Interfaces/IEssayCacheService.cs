namespace Essays.Writer.Application.Services.Interfaces;

public interface IEssayCacheService
{
    /// <summary>
    /// Deletes an essay from the cache using the provided unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the essay to be deleted from the cache.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task DeleteEssay(Guid id);
}