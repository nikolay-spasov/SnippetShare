namespace SnippetShare.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class snippetAuthorCanBeNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Snippets", "UserId", "dbo.UserProfile");
            DropIndex("dbo.Snippets", new[] { "UserId" });
            AlterColumn("dbo.Snippets", "UserId", c => c.Int());
            CreateIndex("dbo.Snippets", "UserId");
            AddForeignKey("dbo.Snippets", "UserId", "dbo.UserProfile", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Snippets", "UserId", "dbo.UserProfile");
            DropIndex("dbo.Snippets", new[] { "UserId" });
            AlterColumn("dbo.Snippets", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Snippets", "UserId");
            AddForeignKey("dbo.Snippets", "UserId", "dbo.UserProfile", "UserId", cascadeDelete: true);
        }
    }
}
