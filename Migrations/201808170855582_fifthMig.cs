namespace MusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fifthMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "LogId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "LogId");
        }
    }
}
