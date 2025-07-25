using System.Diagnostics;
using Essays.Core.Data.Data;
using Essays.Core.Models.Models;
using Essays.Writer.Application.Diagnostics;
using Essays.Writer.Application.Extensions;
using Essays.Writer.Application.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Essays.Writer.Application.Repositories;

public class EssayWriterRepository(EssaysContext essaysContext) : IEssayWriterRepository
{
    /// <inheritdoc/>
    public async Task<Essay?> CreateEssay(Essay essay, CancellationToken cancellationToken = default)
    {
        Activity.Current?.EnrichWithEssay(essay);

        await essaysContext.Essays.AddAsync(essay, cancellationToken);
        var rowsAffected = await essaysContext.SaveChangesAsync(cancellationToken);

        if (rowsAffected > 0)
        {
            ApplicationDiagnostics.EssaysCreatedCounter.Add(1);
            return essay;
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<Essay?> UpdateEssay(Essay essay, CancellationToken cancellationToken = default)
    {
        Activity.Current?.EnrichWithEssay(essay);

        var essayToUpdate = await essaysContext.Essays
            .Include(e => e.Authors)
            .SingleOrDefaultAsync(e => e.Id == essay.Id, cancellationToken);

        if (essayToUpdate == null)
        {
            return null;
        }

        essayToUpdate.Title = essay.Title;
        essayToUpdate.CompressedBody = essay.CompressedBody;
        essayToUpdate.Authors.Clear();
        essayToUpdate.Authors.AddRange(essay.Authors);

        var rowsAffected = await essaysContext.SaveChangesAsync(cancellationToken);

        if (rowsAffected > 0)
        {
            return essayToUpdate;
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteEssay(Guid id, CancellationToken cancellationToken = default)
    {
        Activity.Current?.EnrichWithEssayId(id);

        var essay = await essaysContext.Essays.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (essay == null)
        {
            return false;
        }

        essaysContext.Essays.Remove(essay);
        var rowsAffected = await essaysContext.SaveChangesAsync(cancellationToken);
        return rowsAffected > 0;
    }


}