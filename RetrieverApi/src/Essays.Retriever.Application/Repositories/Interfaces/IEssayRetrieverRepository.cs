using Essays.Core.Models.Models;

namespace Essays.Retriever.Application.Repositories.Interfaces;

public interface IEssayRetrieverRepository
{
    public Task<Essay?> GetEssay(Guid id, CancellationToken cancellationToken = default);
}