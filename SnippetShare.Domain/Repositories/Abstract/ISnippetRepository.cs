namespace SnippetShare.Domain.Repositories.Abstract
{
    using System.Linq;
    using SnippetShare.Domain.Entities;

    public interface ISnippetRepository
    {
        IQueryable<Snippet> Snippets { get; }

        Snippet GetById(int id);

        void Update(Snippet snippetToUpdate);

        void Add(Snippet snippetToAdd);

        void Save();
    }
}
