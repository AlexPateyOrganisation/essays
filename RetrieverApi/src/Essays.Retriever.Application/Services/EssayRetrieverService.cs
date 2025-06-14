using Essays.Retriever.Application.Models;
using Essays.Retriever.Application.Repositories.Interfaces;
using Essays.Retriever.Application.Services.Interfaces;

namespace Essays.Retriever.Application.Services;

public class EssayRetrieverService(IEssayRetrieverRepository essayRetrieverRepository) : IEssayRetrieverService
{
    public async Task<Essay?> GetEssay(Guid id)
    {
        var essay = await essayRetrieverRepository.GetEssay(id);
        return essay;
    }
}