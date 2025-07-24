using Essays.Core.Models.Models;

namespace Essays.Writer.Application.Repositories.Interfaces;

public interface IEssayWriterRepository
{
    public Task<bool> CreateEssay(Essay essay, CancellationToken cancellationToken = default);
    public Task<Essay?> UpdateEssay(Essay essay, CancellationToken cancellationToken = default);
    public Task<bool> DeleteEssay(Guid id, CancellationToken cancellationToken = default);
}