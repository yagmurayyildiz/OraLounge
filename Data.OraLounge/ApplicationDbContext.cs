using Data.OraLounge.Configuration;
using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.OraLounge
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("Default")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Data.OraLounge.Migrations.Configuration>());
        }

        //internal ApplicationDbContext(string nameOrConnectionString)
        //    : base(nameOrConnectionString)
        //{
        //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Data.OraLounge.Migrations.Configuration>());
        //}

        internal IDbSet<Category> Categories { get; set; }
        internal IDbSet<Product> Products { get; set; }
        internal IDbSet<ProductImage> ProductImages { get; set; }
        internal IDbSet<User> Users { get; set; }
        internal IDbSet<Booking> Bookings { get; set; }
        internal IDbSet<Table> Tables { get; set; }
        //internal IDbSet<Role> Roles { get; set; }
        //internal IDbSet<ExternalLogin> Logins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ProductImageConfiguration());
            //modelBuilder.Configurations.Add(new UserConfiguration());
            //modelBuilder.Configurations.Add(new RoleConfiguration());
            //modelBuilder.Configurations.Add(new ExternalLoginConfiguration());
            //modelBuilder.Configurations.Add(new ClaimConfiguration());
        }
    }
}
