namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TradingGoods", "GoodName", c => c.String(nullable: false));
            AlterColumn("dbo.TradingGoods", "ShortDetail", c => c.String(nullable: false));
            AlterColumn("dbo.TradingGoods", "LongDetail", c => c.String(nullable: false));
            AlterColumn("dbo.TradingGoods", "RetailRate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TradingGoods", "RetailRate", c => c.Int());
            AlterColumn("dbo.TradingGoods", "LongDetail", c => c.String());
            AlterColumn("dbo.TradingGoods", "ShortDetail", c => c.String());
            AlterColumn("dbo.TradingGoods", "GoodName", c => c.String());
        }
    }
}
