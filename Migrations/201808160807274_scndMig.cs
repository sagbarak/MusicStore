namespace MusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scndMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "bName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "bName");
        }
    }
}
