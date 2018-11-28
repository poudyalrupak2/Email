using Newspaper.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DemoApplication.Models.DAL
{
    public class DemoDbContext:DbContext
    {
        public DemoDbContext() : base("DefaultConnection")
        {
        }
        public DbSet<CustomerSuperAdmin> Costumers { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<TradingGoods> TradingGoods { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Package> packages { get; set; }
        public DbSet<PImage> pImages { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<AddToCart> addToCarts { get; set; }
        public DbSet<Vat> vat { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<INVProduct> INVProducts { get; set; }
        public DbSet<InvoiceGenerate> InvoiceGenerates { get; set; }
        public DbSet<AssignPackage> AssignPackages { get; set; }
    }
}