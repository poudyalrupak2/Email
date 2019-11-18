namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requireddeleted : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "NationalIdNo", c => c.String());
            AlterColumn("dbo.Customers", "State", c => c.String());
            AlterColumn("dbo.Customers", "Country", c => c.String());
            AlterColumn("dbo.Customers", "ZipCode", c => c.String());
            AlterColumn("dbo.Packages", "PackageCode", c => c.String());
            AlterColumn("dbo.Packages", "PackageName", c => c.String());
            AlterColumn("dbo.Packages", "From", c => c.String());
            AlterColumn("dbo.Packages", "TO", c => c.String());
            AlterColumn("dbo.Packages", "BegainDate", c => c.String());
            AlterColumn("dbo.Admin", "Phone1", c => c.String());
            AlterColumn("dbo.Admin", "CustomerType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admin", "CustomerType", c => c.String(nullable: false));
            AlterColumn("dbo.Admin", "Phone1", c => c.String(nullable: false));
            AlterColumn("dbo.Packages", "BegainDate", c => c.String(nullable: false));
            AlterColumn("dbo.Packages", "TO", c => c.String(nullable: false));
            AlterColumn("dbo.Packages", "From", c => c.String(nullable: false));
            AlterColumn("dbo.Packages", "PackageName", c => c.String(nullable: false));
            AlterColumn("dbo.Packages", "PackageCode", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "ZipCode", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "State", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "NationalIdNo", c => c.String(nullable: false));
        }
    }
}
