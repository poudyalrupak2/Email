namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustumerId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                        Categoty = c.String(nullable: false),
                        email1 = c.String(nullable: false),
                        email2 = c.String(),
                        Phone1 = c.String(nullable: false),
                        PanNo = c.Long(),
                        URL = c.String(),
                        CustomerType = c.String(),
                        Detail = c.String(),
                        WorkingArea = c.String(),
                        NatureOfWork = c.String(),
                        CitizenShipPhoto = c.String(),
                        CompanyDocument = c.String(),
                        Logo = c.String(),
                        PpsizePhoto = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        randompass = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logins");
            DropTable("dbo.Admin");
        }
    }
}
