namespace SnippetShare.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class snippetIdChangedToLong : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Snippets");
            AlterColumn("dbo.Snippets", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Snippets", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Snippets");
            AlterColumn("dbo.Snippets", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Snippets", "Id");
        }
    }
}
