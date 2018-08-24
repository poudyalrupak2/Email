namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rupak : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admin1", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.Admin1", "State", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admin1", "State", c => c.String());
            AlterColumn("dbo.Admin1", "Country", c => c.String());
        }
    }
}
