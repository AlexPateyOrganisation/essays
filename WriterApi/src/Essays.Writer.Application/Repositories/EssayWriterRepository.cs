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
    public async Task<Essay?> CreateEssay(Essay essay, CancellationToken cancellationToken = default)
    {
        Activity.Current?.EnrichWithEssay(essay);

        var processedAuthors = await EnsureAuthors(essay.Authors, cancellationToken);

        essay.Authors.Clear();
        essay.Authors.AddRange(processedAuthors);

        await essaysContext.Essays.AddAsync(essay, cancellationToken);
        var rowsAffected = await essaysContext.SaveChangesAsync(cancellationToken);

        if (rowsAffected > 0)
        {
            ApplicationDiagnostics.EssaysCreatedCounter.Add(1);
            return essay;
        }

        return null;
    }

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

        var processedAuthors = await EnsureAuthors(essay.Authors, cancellationToken);

        essayToUpdate.Title = essay.Title;
        essayToUpdate.CompressedBody = essay.CompressedBody;
        essayToUpdate.Authors.Clear();
        essayToUpdate.Authors.AddRange(processedAuthors);

        var rowsAffected = await essaysContext.SaveChangesAsync(cancellationToken);

        if (rowsAffected > 0)
        {
            return essayToUpdate;
        }

        return null;
    }

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

    private async Task<List<Author>> EnsureAuthors(List<Author> authors, CancellationToken cancellationToken = default)
    {
        try
        {
            List<Author> processedAuthors = [];
            var authorSlugs = authors.Select(a => a.Slug).ToList();

            var existingAuthors = await essaysContext.Authors.Where(a =>
                authorSlugs.Contains(a.Slug)).ToListAsync(cancellationToken);

            foreach (var author in authors)
            {
                var existingAuthor = existingAuthors.SingleOrDefault(a => a.Slug == author.Slug);

                if (existingAuthor is null)
                {
                    await essaysContext.Authors.AddAsync(author, cancellationToken);
                    processedAuthors.Add(author);
                }
                else
                {
                    processedAuthors.Add(existingAuthor);
                }
            }

            return processedAuthors;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}