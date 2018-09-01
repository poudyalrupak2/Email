namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageCode = c.String(),
                        PackageName = c.String(),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                        From = c.String(),
                        TO = c.String(),
                        BegainDate = c.String(),
                        Duration = c.String(),
                        Rate1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Images = c.String(),
                        thumbnail = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        EditedBy = c.String(),
                        EditedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Packages");
        }
    }
}
