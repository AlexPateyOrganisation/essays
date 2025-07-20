using System.Text.Json;
using Essays.Core.Models.Models;
using Essays.Retriever.Application.Services.Interfaces;
using StackExchange.Redis;

namespace Essays.Retriever.Application.Services;

public class EssayCacheService(IConnectionMultiplexer redisConnectionMultiplexer) : IEssayCacheService
{
    private readonly IDatabase _redisDatabase = redisConnectionMultiplexer.GetDatabase();

    public async Task<Essay?> GetEssay(Guid id)
    {
        var cacheKey = $"essay:{id}";
        var cachedEssay = await _redisDatabase.StringGetAsync(cacheKey);

        if (!cachedEssay.HasValue) return null;
        var essay = JsonSerializer.Deserialize<Essay>(cachedEssay!);
        return essay;

    }

    public async Task CacheEssay(Essay? essay)
    {
        if (essay != null)
        {
            var cacheKey = $"essay:{essay.Id}";
            var essayJson = JsonSerializer.Serialize(essay);
            await _redisDatabase.StringSetAsync(cacheKey, essayJson, expiry: TimeSpan.FromDays(2));
        }
    }
}