using Essays.Core.Models.Models;

namespace Essays.Writer.Application.Services.Interfaces;

public interface IEssayWriterService
{
    public Task<bool> CreateEssay(Essay essay, CancellationToken cancellationToken = default);
    public Task<bool> UpdateEssay(Essay essay, CancellationToken cancellationToken = default);
    public Task<bool> DeleteEssay(Guid id, CancellationToken cancellationToken = default);
}