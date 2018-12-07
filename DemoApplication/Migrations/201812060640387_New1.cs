namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TradingGoods", "GoodId", c => c.String(nullable: false));
            AlterColumn("dbo.TradingGoods", "WholesaleRate", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TradingGoods", "WholesaleRate", c => c.Int(nullable: false));
            AlterColumn("dbo.TradingGoods", "GoodId", c => c.Int(nullable: false));
        }
    }
}
