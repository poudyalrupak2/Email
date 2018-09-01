namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Packages", "EditedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Packages", "EditedDate", c => c.DateTime(nullable: false));
        }
    }
}
