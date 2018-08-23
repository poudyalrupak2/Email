namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admin1", "ServiceProvidedDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Admin1", "DOB", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Admin", "RegisterDate", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.Admin1", "ProvidedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admin1", "ProvidedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Admin", "RegisterDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Admin1", "DOB", c => c.DateTime(nullable: false));
            DropColumn("dbo.Admin1", "ServiceProvidedDate");
        }
    }
}
