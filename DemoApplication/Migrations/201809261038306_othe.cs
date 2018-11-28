namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class othe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TradingGoods", "ExpiryDate", c => c.DateTime());
            AlterColumn("dbo.Packages", "PackageCode", c => c.String(nullable: false));
            AlterColumn("dbo.Packages", "PackageName", c => c.String(nullable: false));
            AlterColumn("dbo.Packages", "From", c => c.String(nullable: false));
            AlterColumn("dbo.Packages", "TO", c => c.String(nullable: false));
            AlterColumn("dbo.Packages", "BegainDate", c => c.String(nullable: false));
            DropColumn("dbo.TradingGoods", "ExpityDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TradingGoods", "ExpityDate", c => c.DateTime());
            AlterColumn("dbo.Packages", "BegainDate", c => c.String());
            AlterColumn("dbo.Packages", "TO", c => c.String());
            AlterColumn("dbo.Packages", "From", c => c.String());
            AlterColumn("dbo.Packages", "PackageName", c => c.String());
            AlterColumn("dbo.Packages", "PackageCode", c => c.String());
            DropColumn("dbo.TradingGoods", "ExpiryDate");
        }
    }
}
