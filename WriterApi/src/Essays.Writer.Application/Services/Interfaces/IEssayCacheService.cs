namespace Essays.Writer.Application.Services.Interfaces;

public interface IEssayCacheService
{
    public Task DeleteEssay(Guid id);
}