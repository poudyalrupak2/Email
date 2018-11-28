namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.INVProducts", "InvoiceId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.INVProducts", "InvoiceId");
        }
    }
}
