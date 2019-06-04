using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartGarcom.Models;
using SmartGarcom.Filters;

namespace SmartGarcom.Models
{
    public class Banco : DbContext
    {
        public Banco(DbContextOptions<Banco> options) : base(options) { }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<TUser> TUsers { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<OrderCard> OrderCards { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Company)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductCategory)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.ProductCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(p => p.Company)
                .WithMany(b => b.ProductCategories)
                .OnDelete(DeleteBehavior.ClientSetNull);


               modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
                modelBuilder.Entity<ProductCategory>().HasQueryFilter(p => !p.IsDeleted);
                modelBuilder.Entity<TUser>().HasQueryFilter(p => !p.IsDeleted);
                modelBuilder.Entity<Company>().HasQueryFilter(p => !p.IsDeleted);
                modelBuilder.Entity<Table>().HasQueryFilter(p => !p.IsDeleted);

            

        }

    }

}





