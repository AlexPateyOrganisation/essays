using Essays.Retriever.Application.Models;
using Essays.Retriever.Application.Repositories.Interfaces;

namespace Essays.Retriever.Application.Repositories;

public class EssayRetrieverRepository : IEssayRetrieverRepository
{
    private readonly Dictionary<Guid, Essay> _essays = new()
    {
        {
            new Guid("2a54b45f-dbd9-476d-b798-7c157081692f"),
            new Essay(new Guid("2a54b45f-dbd9-476d-b798-7c157081692f"), "Title 1", [], "Author 1", DateTime.Now)
        },
        {
            new Guid("f412e047-6954-4928-825b-99aeb65dbbdf"),
            new Essay(new Guid("f412e047-6954-4928-825b-99aeb65dbbdf"), "Title 2", [], "Author 2", DateTime.Now)
        }
    };

    public async Task<Essay?> GetEssay(Guid id)
    {
        var essay = _essays.GetValueOrDefault(id);
        return essay;
    }
}