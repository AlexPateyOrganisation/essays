using Essays.Core.Data.Data;
using Essays.Core.Models.Models;
using Essays.Retriever.Application.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Essays.Retriever.Application.Repositories;

public class EssayRetrieverRepository(EssaysContext essaysContext) : IEssayRetrieverRepository
{
    public async Task<Essay?> GetEssay(Guid id, CancellationToken cancellationToken = default)
    {
        var essay = await essaysContext.Essays.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        return essay;
    }
}