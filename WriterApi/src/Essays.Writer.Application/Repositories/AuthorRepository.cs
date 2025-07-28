using Essays.Core.Data.Data;
using Essays.Core.Models.Models;
using Essays.Writer.Application.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Essays.Writer.Application.Repositories;

public class AuthorRepository(EssaysContext essaysContext) : IAuthorRepository
{
    /// <inheritdoc/>
    public async Task<List<Author>> EnsureAuthors(List<Author> authors, CancellationToken cancellationToken = default)
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
}