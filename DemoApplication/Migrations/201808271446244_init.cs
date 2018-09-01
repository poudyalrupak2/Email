namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        NationalIdType = c.String(nullable: false),
                        NationalIdNo = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false, storeType: "date"),
                        Email = c.String(nullable: false),
                        Phone = c.Long(nullable: false),
                        Country = c.String(nullable: false),
                        State = c.String(nullable: false),
                        TypeOfService = c.String(),
                        ServiceProvidedDate = c.DateTime(nullable: false, storeType: "date"),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustumerId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        RegisterDate = c.DateTime(nullable: false, storeType: "date"),
                        Categoty = c.String(nullable: false),
                        email1 = c.String(nullable: false),
                        email2 = c.String(),
                        Phone1 = c.String(nullable: false),
                        PanNo = c.Long(),
                        URL = c.String(),
                        CustomerType = c.String(nullable: false),
                        Detail = c.String(),
                        WorkingArea = c.String(),
                        NatureOfWork = c.String(),
                        CitizenShipPhoto = c.String(),
                        CompanyDocument = c.String(),
                        Logo = c.String(),
                        PpsizePhoto = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        Size = c.Long(nullable: false),
                        Path = c.String(),
                        TradingGoodsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.TradingGoods", t => t.TradingGoodsId, cascadeDelete: true)
                .Index(t => t.TradingGoodsId);
            
            CreateTable(
                "dbo.TradingGoods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoodId = c.Int(nullable: false),
                        GoodName = c.String(),
                        ShortDetail = c.String(),
                        LongDetail = c.String(),
                        Thumbail = c.String(),
                        WholesaleRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        CoupenCode = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        EditedBy = c.String(),
                        EditedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        randompass = c.String(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "TradingGoodsId", "dbo.TradingGoods");
            DropIndex("dbo.Images", new[] { "TradingGoodsId" });
            DropTable("dbo.Logins");
            DropTable("dbo.TradingGoods");
            DropTable("dbo.Images");
            DropTable("dbo.Admin");
            DropTable("dbo.Admin1");
        }
    }
}
