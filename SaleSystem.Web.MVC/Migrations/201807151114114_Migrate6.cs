namespace SaleSystem.Web.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrate6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
