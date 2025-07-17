using Essays.Core.Models.Models;

namespace Essays.Writer.Application.Repositories.Interfaces;

public interface IEssayWriterRepository
{
    public Task<bool> CreateEssay(Essay essay);
}