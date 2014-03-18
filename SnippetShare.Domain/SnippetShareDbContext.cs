namespace SnippetShare.Domain
{
    using System.Data.Entity;
    using SnippetShare.Domain.Entities;
    using SnippetShare.Domain.Migrations;

    public class SnippetShareDbContext : DbContext
    {
        public SnippetShareDbContext()
            : base("DefaultConnection")
        {
        }

        public SnippetShareDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Snippet> Snippets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SnippetShareDbContext, Configuration>());
        }
    }
}
