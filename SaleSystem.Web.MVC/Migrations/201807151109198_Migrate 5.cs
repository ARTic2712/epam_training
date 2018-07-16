namespace SaleSystem.Web.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrate5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sales", "ManagerId");
            DropColumn("dbo.Sales", "ClientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "ClientId", c => c.Int(nullable: false));
            AddColumn("dbo.Sales", "ManagerId", c => c.Int(nullable: false));
        }
    }
}
