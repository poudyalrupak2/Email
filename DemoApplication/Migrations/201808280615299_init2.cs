namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TradingGoods", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.TradingGoods", "EditedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TradingGoods", "EditedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TradingGoods", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
