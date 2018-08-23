namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admin1", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Admin1", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Admin", "CustomerType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admin", "CustomerType", c => c.String());
            AlterColumn("dbo.Admin1", "Email", c => c.String());
            AlterColumn("dbo.Admin1", "Name", c => c.String());
        }
    }
}
