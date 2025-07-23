using Essays.Core.Models.Models;

namespace Essays.Retriever.Application.Services.Interfaces;

public interface IEssayCacheService
{
    public Task<Essay?> GetEssay(Guid id);
    public Task CacheEssay(Essay? essay);
}