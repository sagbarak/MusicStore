namespace MusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11thMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "StoreName", c => c.String(nullable: false));
            AddColumn("dbo.Items", "StoreName", c => c.String());
            DropColumn("dbo.Branches", "Name");
            DropColumn("dbo.Items", "bName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "bName", c => c.String());
            AddColumn("dbo.Branches", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Items", "StoreName");
            DropColumn("dbo.Branches", "StoreName");
        }
    }
}
