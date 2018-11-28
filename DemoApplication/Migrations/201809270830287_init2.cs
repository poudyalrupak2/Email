namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignPackages",
                c => new
                    {
                        AssignPackageId = c.Int(nullable: false, identity: true),
                        PackageId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        AssignDate = c.String(),
                    })
                .PrimaryKey(t => t.AssignPackageId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .Index(t => t.PackageId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignPackages", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.AssignPackages", "CustomerId", "dbo.Customers");
            DropIndex("dbo.AssignPackages", new[] { "CustomerId" });
            DropIndex("dbo.AssignPackages", new[] { "PackageId" });
            DropTable("dbo.AssignPackages");
        }
    }
}
