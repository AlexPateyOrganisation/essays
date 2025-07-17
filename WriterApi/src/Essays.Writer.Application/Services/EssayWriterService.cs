using Essays.Core.Models.Models;
using Essays.Writer.Application.Repositories.Interfaces;
using Essays.Writer.Application.Services.Interfaces;

namespace Essays.Writer.Application.Services;

public class EssayWriterService(IEssayWriterRepository essayWriterRepository) : IEssayWriterService
{
    public async Task<bool> CreateEssay(Essay essay)
    {
        var isCreated = await essayWriterRepository.CreateEssay(essay);
        return isCreated;
    }
}