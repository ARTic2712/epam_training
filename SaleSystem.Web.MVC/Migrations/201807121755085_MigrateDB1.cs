namespace SaleSystem.Web.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DateSale = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        Client_Id = c.String(maxLength: 128),
                        Manager_Id = c.String(maxLength: 128),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Client_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Manager_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.Manager_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Sales", "Manager_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sales", "Client_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Sales", new[] { "Product_Id" });
            DropIndex("dbo.Sales", new[] { "Manager_Id" });
            DropIndex("dbo.Sales", new[] { "Client_Id" });
            DropTable("dbo.Sales");
            DropTable("dbo.Products");
        }
    }
}
