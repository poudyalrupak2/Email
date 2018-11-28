namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class date : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.cart", "GoodName", c => c.String(nullable: false));
            AlterColumn("dbo.cart", "price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.cart", "price", c => c.Int());
            AlterColumn("dbo.cart", "GoodName", c => c.String());
        }
    }
}
