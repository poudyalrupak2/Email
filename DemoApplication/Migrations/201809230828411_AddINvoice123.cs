namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddINvoice123 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.INVProducts", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.INVProducts", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.INVProducts", "Quantity");
            DropColumn("dbo.INVProducts", "Rate");
        }
    }
}
