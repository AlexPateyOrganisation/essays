using Essays.Core.Models.Models;
using Essays.Writer.Application.Repositories.Interfaces;
using Essays.Writer.Application.Services.Interfaces;

namespace Essays.Writer.Application.Services;

public class EssayWriterService(IEssayCacheService essayCacheService, IEssayWriterRepository essayWriterRepository) : IEssayWriterService
{
    public async Task<bool> CreateEssay(Essay essay, CancellationToken cancellationToken = default)
    {
        var isCreated = await essayWriterRepository.CreateEssay(essay, cancellationToken);
        return isCreated;
    }

    public async Task<bool> UpdateEssay(Essay essay, CancellationToken cancellationToken = default)
    {
        var isUpdated = await essayWriterRepository.UpdateEssay(essay, cancellationToken);

        if (isUpdated)
        {
            await essayCacheService.DeleteEssay(essay.Id);
        }

        return isUpdated;
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