namespace SnippetShare.Domain.Repositories.Concrete
{
    using System.Linq;
    using System.Data.Entity;
    using SnippetShare.Domain.Entities;
    using SnippetShare.Domain.Repositories.Abstract;

    public class SnippetRepository : ISnippetRepository
    {
        private SnippetShareDbContext db;

        public SnippetRepository()
        {
            this.db = new SnippetShareDbContext();
        }

        public IQueryable<Snippet> Snippets
        {
            get
            {
                return db.Snippets.AsQueryable();
            }
        }

        public void Update(Snippet snippetToUpdate)
        {
            var snippet = db.Entry<Snippet>(snippetToUpdate);
            snippet.State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Add(Snippet snippetToAdd)
        {
            db.Snippets.Add(snippetToAdd);
            db.SaveChanges();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public Snippet GetById(long id)
        {
            return db.Snippets.Find(id);
        }
    }
}
