namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class others : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.INVProducts", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.INVProducts", "Other");
        }
        
        public override void Down()
        {
            AddColumn("dbo.INVProducts", "Other", c => c.Int(nullable: false));
            DropColumn("dbo.INVProducts", "Total");
        }
    }
}
