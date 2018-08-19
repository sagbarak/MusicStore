namespace MusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trdMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "City", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "City", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "City");
            DropColumn("dbo.Branches", "City");
        }
    }
}
