using Essays.Core.Models.Models;

namespace Essays.Retriever.Application.Services.Interfaces;

public interface IEssayRetrieverService
{
    public Task<Essay?> GetEssay(Guid id, CancellationToken cancellationToken = default);
}