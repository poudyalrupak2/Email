namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.INVProducts", "Other", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.INVProducts", "Other");
        }
    }
}
