namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvoice1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.INVProducts", "Invoice_Id", "dbo.Invoices");
            DropForeignKey("dbo.INVProducts", "TradingGoods_Id", "dbo.TradingGoods");
            DropIndex("dbo.Invoices", new[] { "Customer_CustomerId" });
            DropIndex("dbo.INVProducts", new[] { "Invoice_Id" });
            DropIndex("dbo.INVProducts", new[] { "TradingGoods_Id" });
           // DropTable("dbo.Invoices");
          //  DropTable("dbo.INVProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.INVProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Invoice_Id = c.Int(),
                        TradingGoods_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.INVProducts", "TradingGoods_Id");
            CreateIndex("dbo.INVProducts", "Invoice_Id");
            CreateIndex("dbo.Invoices", "Customer_CustomerId");
            AddForeignKey("dbo.INVProducts", "TradingGoods_Id", "dbo.TradingGoods", "Id");
            AddForeignKey("dbo.INVProducts", "Invoice_Id", "dbo.Invoices", "Id");
            AddForeignKey("dbo.Invoices", "Customer_CustomerId", "dbo.Customers", "CustomerId");
        }
    }
}
