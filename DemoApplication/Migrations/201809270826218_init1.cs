namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AssignPackages", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.AssignPackages", "Package_Id", "dbo.Packages");
            DropIndex("dbo.AssignPackages", new[] { "Customer_CustomerId" });
            DropIndex("dbo.AssignPackages", new[] { "Package_Id" });
            DropTable("dbo.AssignPackages");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.AssignPackageId);
            
            CreateIndex("dbo.AssignPackages", "Package_Id");
            CreateIndex("dbo.AssignPackages", "Customer_CustomerId");
            AddForeignKey("dbo.AssignPackages", "Package_Id", "dbo.Packages", "Id");
            AddForeignKey("dbo.AssignPackages", "Customer_CustomerId", "dbo.Customers", "CustomerId");
        }
    }
}
