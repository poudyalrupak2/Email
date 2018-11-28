namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddINvoice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceNo = c.String(),
                        Vat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Other = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrandTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId)
                .Index(t => t.Customer_CustomerId);
            
            CreateTable(
                "dbo.INVProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Invoice_Id = c.Int(),
                        TradingGoods_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.Invoice_Id)
                .ForeignKey("dbo.TradingGoods", t => t.TradingGoods_Id)
                .Index(t => t.Invoice_Id)
                .Index(t => t.TradingGoods_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.INVProducts", "TradingGoods_Id", "dbo.TradingGoods");
            DropForeignKey("dbo.INVProducts", "Invoice_Id", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "Customer_CustomerId", "dbo.Customers");
            DropIndex("dbo.INVProducts", new[] { "TradingGoods_Id" });
            DropIndex("dbo.INVProducts", new[] { "Invoice_Id" });
            DropIndex("dbo.Invoices", new[] { "Customer_CustomerId" });
            DropTable("dbo.INVProducts");
            DropTable("dbo.Invoices");
        }
    }
}
