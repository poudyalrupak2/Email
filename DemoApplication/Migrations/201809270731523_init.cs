namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignPackages",
                c => new
                    {
                        AssignPackageId = c.Int(nullable: false, identity: true),
                        AssignDate = c.String(),
                        Customer_CustomerId = c.Int(),
                        Package_Id = c.Int(),
                    })
                .PrimaryKey(t => t.AssignPackageId)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId)
                .ForeignKey("dbo.Packages", t => t.Package_Id)
                .Index(t => t.Customer_CustomerId)
                .Index(t => t.Package_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignPackages", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.AssignPackages", "Customer_CustomerId", "dbo.Customers");
            DropIndex("dbo.AssignPackages", new[] { "Package_Id" });
            DropIndex("dbo.AssignPackages", new[] { "Customer_CustomerId" });
            DropTable("dbo.AssignPackages");
        }
    }
}
