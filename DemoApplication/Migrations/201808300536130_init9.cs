namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PackageImage",
                c => new
                    {
                        PImageId = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        Size = c.Long(nullable: false),
                        Path = c.String(),
                        PackageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PImageId)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .Index(t => t.PackageId);
            
            DropColumn("dbo.Packages", "Images");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "Images", c => c.String());
            DropForeignKey("dbo.PackageImage", "PackageId", "dbo.Packages");
            DropIndex("dbo.PackageImage", new[] { "PackageId" });
            DropTable("dbo.PackageImage");
        }
    }
}
