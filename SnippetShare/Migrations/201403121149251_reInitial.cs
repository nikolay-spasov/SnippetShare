namespace SnippetShare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Snippets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        DatePublished = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Snippets", "UserId", "dbo.UserProfile");
            DropIndex("dbo.Snippets", new[] { "UserId" });
            DropTable("dbo.UserProfile");
            DropTable("dbo.Snippets");
        }
    }
}
