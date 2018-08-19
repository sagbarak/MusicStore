namespace MusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201808140714529_InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Artist", c => c.String(nullable: false));
            AddColumn("dbo.Items", "Genre", c => c.String(nullable: false));
            AddColumn("dbo.Items", "Year", c => c.Int(nullable: false));
            DropColumn("dbo.Items", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Category", c => c.String(nullable: false));
            DropColumn("dbo.Items", "Year");
            DropColumn("dbo.Items", "Genre");
            DropColumn("dbo.Items", "Artist");
        }
    }
}
