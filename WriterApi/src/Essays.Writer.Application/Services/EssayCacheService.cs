using Essays.Writer.Application.Services.Interfaces;
using StackExchange.Redis;

namespace Essays.Writer.Application.Services;

public class EssayCacheService(IConnectionMultiplexer redisConnectionMultiplexer) : IEssayCacheService
{
    private readonly IDatabase _redisDatabase = redisConnectionMultiplexer.GetDatabase();

    /// <inheritdoc/>
    public async Task DeleteEssay(Guid id)
        => await _redisDatabase.KeyDeleteAsync($"essay:{id}");
}