namespace DemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Operation = c.String(),
                        CreatedBy = c.String(),
                        category = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.cart",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        GoodName = c.String(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Int(nullable: false),
                        VatAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        vat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SessonId = c.String(),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateTable(
                "dbo.AssignPackages",
                c => new
                    {
                        AssignPackageId = c.Int(nullable: false, identity: true),
                        PackageId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        AssignDate = c.String(),
                    })
                .PrimaryKey(t => t.AssignPackageId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .Index(t => t.PackageId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Middlename = c.String(),
                        LastName = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false, storeType: "date"),
                        email1 = c.String(nullable: false),
                        email2 = c.String(),
                        phone = c.Long(nullable: false),
                        phone2 = c.Long(nullable: false),
                        NationalIdNo = c.String(nullable: false),
                        tole = c.String(),
                        homeno = c.String(),
                        State = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        EditedBy = c.String(),
                        EditedDate = c.DateTime(),
                        category = c.String(),
                        CustomerCategory = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageCode = c.String(nullable: false),
                        PackageName = c.String(nullable: false),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                        From = c.String(nullable: false),
                        TO = c.String(nullable: false),
                        BegainDate = c.String(nullable: false),
                        Duration = c.Int(nullable: false),
                        Rate1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        thumbnail = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        EditedBy = c.String(),
                        EditedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PackageImage",
                c => new
                    {
                        PImageId = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        Size = c.Long(nullable: false),
                        Path = c.String(),
                        PackageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PImageId)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .Index(t => t.PackageId);
            
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustumerId = c.String(nullable: false),
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
                        GoodId = c.String(nullable: false),
                        GoodName = c.String(nullable: false),
                        ShortDetail = c.String(nullable: false),
                        LongDetail = c.String(nullable: false),
                        ExpiryDate = c.DateTime(),
                        MfdDate = c.DateTime(),
                        Thumbail = c.String(),
                        RetailRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WholesaleRate = c.Int(),
                        Status = c.String(),
                        Quantity = c.Int(nullable: false),
                        CoupenCode = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        EditedBy = c.String(),
                        EditedDate = c.DateTime(),
                        Vat = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvoiceGenerates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceNo = c.String(),
                        CustomerId = c.Int(nullable: false),
                        Vat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Other = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrandTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.ProductInvoice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.String(),
                        TradingGoodsId = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Invoice_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.Invoice_Id)
                .ForeignKey("dbo.TradingGoods", t => t.TradingGoodsId, cascadeDelete: true)
                .Index(t => t.TradingGoodsId)
                .Index(t => t.Invoice_Id);
            
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
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CostumerName = c.String(),
                        DOB = c.DateTime(nullable: false, storeType: "date"),
                        Email = c.String(),
                        Category = c.String(),
                        From = c.String(),
                        TO = c.String(),
                        DepartureDate = c.DateTime(nullable: false, storeType: "date"),
                        DepartureTime = c.Time(precision: 7),
                        ReturnDate = c.DateTime(storeType: "date"),
                        ReturnTime = c.Time(precision: 7),
                        NoOfPersons = c.Int(nullable: false),
                        NoOfAdult = c.Int(),
                        NoOfChildern = c.Int(),
                        NoOfOldPerson = c.Int(),
                        Nationality = c.String(),
                        TotalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(storeType: "date"),
                        EditedBy = c.String(),
                        EditedDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VatPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductInvoice", "TradingGoodsId", "dbo.TradingGoods");
            DropForeignKey("dbo.ProductInvoice", "Invoice_Id", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Images", "TradingGoodsId", "dbo.TradingGoods");
            DropForeignKey("dbo.AssignPackages", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.PackageImage", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.AssignPackages", "CustomerId", "dbo.Customers");
            DropIndex("dbo.ProductInvoice", new[] { "Invoice_Id" });
            DropIndex("dbo.ProductInvoice", new[] { "TradingGoodsId" });
            DropIndex("dbo.Invoices", new[] { "CustomerId" });
            DropIndex("dbo.Images", new[] { "TradingGoodsId" });
            DropIndex("dbo.PackageImage", new[] { "PackageId" });
            DropIndex("dbo.AssignPackages", new[] { "CustomerId" });
            DropIndex("dbo.AssignPackages", new[] { "PackageId" });
            DropTable("dbo.Vats");
            DropTable("dbo.Tickets");
            DropTable("dbo.Logins");
            DropTable("dbo.ProductInvoice");
            DropTable("dbo.Invoices");
            DropTable("dbo.InvoiceGenerates");
            DropTable("dbo.TradingGoods");
            DropTable("dbo.Images");
            DropTable("dbo.Admin");
            DropTable("dbo.PackageImage");
            DropTable("dbo.Packages");
            DropTable("dbo.Customers");
            DropTable("dbo.AssignPackages");
            DropTable("dbo.cart");
            DropTable("dbo.ActivityLogs");
        }
    }
}
