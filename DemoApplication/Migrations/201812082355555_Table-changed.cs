namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tablechanged : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.INVProducts", newName: "ProductInvoice");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ProductInvoice", newName: "INVProducts");
        }
    }
}
