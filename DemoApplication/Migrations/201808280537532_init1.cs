namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TradingGoods", "RetailRate", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TradingGoods", "RetailRate");
        }
    }
}
