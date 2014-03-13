using SnippetShare.DataAccess.Entities;
using SnippetShare.Migrations;
using System.Data.Entity;

namespace SnippetShare.DataAccess
{
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SnippetShareDbContext, Configuration>());
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Snippet> Snippets { get; set; }
    }
}