namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TradingCompletes", "CreatedBy", c => c.String());
            AddColumn("dbo.TradingCompletes", "CreatedDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TradingCompletes", "CreatedDate");
            DropColumn("dbo.TradingCompletes", "CreatedBy");
        }
    }
}
