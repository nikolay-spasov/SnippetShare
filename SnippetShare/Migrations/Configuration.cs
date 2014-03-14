namespace SnippetShare.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<SnippetShare.DataAccess.SnippetShareDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SnippetShare.DataAccess.SnippetShareDbContext context)
        {
        }
    }
}
