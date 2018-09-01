namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Middlename = c.String(),
                        LastName = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        email1 = c.String(nullable: false),
                        email2 = c.String(),
                        phone = c.Long(nullable: false),
                        phone2 = c.Long(nullable: false),
                        NationalIdNo = c.String(nullable: false),
                        tole = c.String(),
                        homeno = c.String(),
                        State = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        EditedBy = c.String(),
                        EditedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.customers");
        }
    }
}
