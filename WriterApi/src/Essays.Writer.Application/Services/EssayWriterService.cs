using Essays.Core.Models.Models;
using Essays.Writer.Application.Repositories.Interfaces;
using Essays.Writer.Application.Services.Interfaces;

namespace Essays.Writer.Application.Services;

public class EssayWriterService(IEssayCacheService essayCacheService, IEssayWriterRepository essayWriterRepository) : IEssayWriterService
{
    public async Task<bool> CreateEssay(Essay essay)
    {
        var isCreated = await essayWriterRepository.CreateEssay(essay);
        return isCreated;
    }

    public async Task<bool> UpdateEssay(Essay essay)
    {
        var isUpdated = await essayWriterRepository.UpdateEssay(essay);

        if (isUpdated)
        {
            await essayCacheService.DeleteEssay(essay.Id);
        }

        return isUpdated;
    }

    public async Task<bool> DeleteEssay(Guid id)
    {
        var isDeleted = await essayWriterRepository.DeleteEssay(id);

        if (isDeleted)
        {
            await essayCacheService.DeleteEssay(id);
        }

        return isDeleted;
    }
}