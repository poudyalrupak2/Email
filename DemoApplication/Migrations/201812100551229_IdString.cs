namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admin", "CustumerId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admin", "CustumerId", c => c.Int(nullable: false));
        }
    }
}
