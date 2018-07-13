namespace SaleSystem.Web.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sales", "Description", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sales", "Description", c => c.String());
        }
    }
}
