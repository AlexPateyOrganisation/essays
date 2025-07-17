using Essays.Core.Models.Models;

namespace Essays.Writer.Application.Services.Interfaces;

public interface IEssayWriterService
{
    public Task<bool> CreateEssay(Essay essay);
}