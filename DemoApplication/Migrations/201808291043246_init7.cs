namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "thumbnail", c => c.String());
            DropColumn("dbo.Packages", "Thumsnail");
            DropTable("dbo.Admin1");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Admin1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        NationalIdType = c.String(nullable: false),
                        NationalIdNo = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false, storeType: "date"),
                        Email = c.String(nullable: false),
                        Phone = c.Long(nullable: false),
                        Country = c.String(nullable: false),
                        State = c.String(nullable: false),
                        TypeOfService = c.String(),
                        ServiceProvidedDate = c.DateTime(nullable: false, storeType: "date"),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Packages", "Thumsnail", c => c.String());
            DropColumn("dbo.Packages", "thumbnail");
        }
    }
}
