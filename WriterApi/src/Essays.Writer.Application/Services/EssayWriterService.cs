using Essays.Core.Models.Models;
using Essays.Writer.Application.Repositories.Interfaces;
using Essays.Writer.Application.Services.Interfaces;

namespace Essays.Writer.Application.Services;

public class EssayWriterService(IEssayCacheService essayCacheService, IEssayWriterRepository essayWriterRepository, IAuthorRepository authorRepository) : IEssayWriterService
{
    /// <inheritdoc/>
    public async Task<Essay?> CreateEssay(Essay essay, CancellationToken cancellationToken = default)
    {
        var processedAuthors = await authorRepository.EnsureAuthors(essay.Authors, cancellationToken);

        essay.Authors.Clear();
        essay.Authors.AddRange(processedAuthors);

        var createdEssay = await essayWriterRepository.CreateEssay(essay, cancellationToken);
        return createdEssay;
    }

    /// <inheritdoc/>
    public async Task<Essay?> UpdateEssay(Essay essay, CancellationToken cancellationToken = default)
    {
        var processedAuthors = await authorRepository.EnsureAuthors(essay.Authors, cancellationToken);

        essay.Authors.Clear();
        essay.Authors.AddRange(processedAuthors);

        var updatedEssay = await essayWriterRepository.UpdateEssay(essay, cancellationToken);

        if (updatedEssay is not null)
        {
            await essayCacheService.DeleteEssay(essay.Id);
        }

        return updatedEssay;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteEssay(Guid id, CancellationToken cancellationToken = default)
    {
        var isDeleted = await essayWriterRepository.DeleteEssay(id, cancellationToken);

        if (isDeleted)
        {
            await essayCacheService.DeleteEssay(id);
        }

        return isDeleted;
    }
}