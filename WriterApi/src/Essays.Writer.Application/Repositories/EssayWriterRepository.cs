using Essays.Core.Data.Data;
using Essays.Core.Models.Models;
using Essays.Writer.Application.Repositories.Interfaces;

namespace Essays.Writer.Application.Repositories;

public class EssayWriterRepository(EssaysContext essaysContext) : IEssayWriterRepository
{
    public async Task<bool> CreateEssay(Essay essay)
    {
        await essaysContext.Essays.AddAsync(essay);
        var rowsAffected = await essaysContext.SaveChangesAsync();
        return rowsAffected > 0;
    }
}