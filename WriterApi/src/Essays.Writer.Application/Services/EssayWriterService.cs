using Essays.Core.Models.Models;
using Essays.Writer.Application.Repositories.Interfaces;
using Essays.Writer.Application.Services.Interfaces;

namespace Essays.Writer.Application.Services;

public class EssayWriterService(IEssayCacheService essayCacheService, IEssayWriterRepository essayWriterRepository) : IEssayWriterService
{
    public async Task<Essay?> CreateEssay(Essay essay, CancellationToken cancellationToken = default)
    {
        var createdEssay = await essayWriterRepository.CreateEssay(essay, cancellationToken);
        return createdEssay;
    }

    public async Task<Essay?> UpdateEssay(Essay essay, CancellationToken cancellationToken = default)
    {
        var updatedEssay = await essayWriterRepository.UpdateEssay(essay, cancellationToken);

        if (updatedEssay is not null)
        {
            await essayCacheService.DeleteEssay(essay.Id);
        }

        return updatedEssay;
    }

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