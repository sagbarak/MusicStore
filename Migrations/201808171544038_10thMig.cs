namespace MusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10thMig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Items", new[] { "Customer_Id" });
            AddColumn("dbo.Items", "Sales", c => c.Int(nullable: false));
            DropColumn("dbo.Items", "Customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Customer_Id", c => c.Int());
            DropColumn("dbo.Items", "Sales");
            CreateIndex("dbo.Items", "Customer_Id");
            AddForeignKey("dbo.Items", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
