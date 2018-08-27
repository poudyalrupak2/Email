namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Images");
            AlterColumn("dbo.Images", "ImageId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Images", "ImageId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Images");
            AlterColumn("dbo.Images", "ImageId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Images", "ImageId");
        }
    }
}
