using Essays.Retriever.Application.Models;

namespace Essays.Retriever.Application.Services.Interfaces;

public interface IEssayRetrieverService
{
    public Task<Essay?> GetEssay(Guid id);
}