namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.INVProducts", "TradingGoods_Id", "dbo.TradingGoods");
            DropIndex("dbo.Invoices", new[] { "Customer_CustomerId" });
            DropIndex("dbo.INVProducts", new[] { "TradingGoods_Id" });
            RenameColumn(table: "dbo.Invoices", name: "Customer_CustomerId", newName: "CustomerId");
            RenameColumn(table: "dbo.INVProducts", name: "TradingGoods_Id", newName: "TradingGoodsId");
            CreateTable(
                "dbo.InvoiceGenerates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.cart", "price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TradingGoods", "RetailRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.INVProducts", "TradingGoodsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Invoices", "CustomerId");
            CreateIndex("dbo.INVProducts", "TradingGoodsId");
            AddForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.INVProducts", "TradingGoodsId", "dbo.TradingGoods", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.INVProducts", "TradingGoodsId", "dbo.TradingGoods");
            DropForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers");
            DropIndex("dbo.INVProducts", new[] { "TradingGoodsId" });
            DropIndex("dbo.Invoices", new[] { "CustomerId" });
            AlterColumn("dbo.INVProducts", "TradingGoodsId", c => c.Int());
            AlterColumn("dbo.Invoices", "CustomerId", c => c.Int());
            AlterColumn("dbo.TradingGoods", "RetailRate", c => c.Int(nullable: false));
            AlterColumn("dbo.cart", "price", c => c.Int(nullable: false));
            DropTable("dbo.InvoiceGenerates");
            RenameColumn(table: "dbo.INVProducts", name: "TradingGoodsId", newName: "TradingGoods_Id");
            RenameColumn(table: "dbo.Invoices", name: "CustomerId", newName: "Customer_CustomerId");
            CreateIndex("dbo.INVProducts", "TradingGoods_Id");
            CreateIndex("dbo.Invoices", "Customer_CustomerId");
            AddForeignKey("dbo.INVProducts", "TradingGoods_Id", "dbo.TradingGoods", "Id");
            AddForeignKey("dbo.Invoices", "Customer_CustomerId", "dbo.Customers", "CustomerId");
        }
    }
}
