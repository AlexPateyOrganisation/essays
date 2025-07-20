using Essays.Core.Data.Data;
using Essays.Core.Models.Models;
using Essays.Writer.Application.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Essays.Writer.Application.Repositories;

public class EssayWriterRepository(EssaysContext essaysContext) : IEssayWriterRepository
{
    public async Task<bool> CreateEssay(Essay essay)
    {
        await essaysContext.Essays.AddAsync(essay);
        var rowsAffected = await essaysContext.SaveChangesAsync();
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateEssay(Essay essay)
    {
        var essayToUpdate = essaysContext.Essays.SingleOrDefault(e => e.Id == essay.Id);

        if (essayToUpdate == null)
        {
            return false;
        }

        essayToUpdate.Title = essay.Title;
        essayToUpdate.CompressedBody = essay.CompressedBody;
        essayToUpdate.Author = essay.Author;

        essaysContext.Essays.Update(essayToUpdate);
        var rowsAffected = await essaysContext.SaveChangesAsync();
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteEssay(Guid id)
    {
        var essay = await essaysContext.Essays.SingleOrDefaultAsync(e => e.Id == id);

        if (essay == null)
        {
            return false;
        }

        essaysContext.Essays.Remove(essay);
        var rowsAffected = await essaysContext.SaveChangesAsync();
        return rowsAffected > 0;
    }
}