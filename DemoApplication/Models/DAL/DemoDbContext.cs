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
        public DbSet<AdminCostumers> AdminCostumers { get; set; }

    }
}