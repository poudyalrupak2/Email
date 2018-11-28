namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TradingComplete : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TradingCompletes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Rate = c.Int(),
                        TradingGoodsId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.TradingGoods", t => t.TradingGoodsId, cascadeDelete: true)
                .Index(t => t.TradingGoodsId)
                .Index(t => t.CustomerId);
            
          //  AddColumn("dbo.cart", "vat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
          //  AddColumn("dbo.cart", "SessonId", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TradingCompletes", "TradingGoodsId", "dbo.TradingGoods");
            DropForeignKey("dbo.TradingCompletes", "CustomerId", "dbo.Customers");
            DropIndex("dbo.TradingCompletes", new[] { "CustomerId" });
            DropIndex("dbo.TradingCompletes", new[] { "TradingGoodsId" });
           // DropColumn("dbo.cart", "SessonId");
           // DropColumn("dbo.cart", "vat");
            DropTable("dbo.TradingCompletes");
        }
    }
}
