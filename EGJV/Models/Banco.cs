﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EGJV.Models;
using EGJV.Filters;

namespace EGJV.Models
{
    public class Banco : DbContext
    {
        public Banco(DbContextOptions<Banco> options) : base(options) { }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<TUser> TUsers { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>()
                .HasOne(p => p.Company)
                .WithMany(b => b.Assets)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Asset>()
                .HasOne(p => p.AssetType)
                .WithMany(b => b.Assets)
                .HasForeignKey(p => p.AssetTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AssetType>()
                .HasOne(p => p.Company)
                .WithMany(b => b.AssetTypes)
                .OnDelete(DeleteBehavior.ClientSetNull);


               modelBuilder.Entity<Asset>().HasQueryFilter(p => !p.IsDeleted);
                modelBuilder.Entity<AssetType>().HasQueryFilter(p => !p.IsDeleted);
                modelBuilder.Entity<TUser>().HasQueryFilter(p => !p.IsDeleted);
                modelBuilder.Entity<Company>().HasQueryFilter(p => !p.IsDeleted);
                modelBuilder.Entity<Ticket>().HasQueryFilter(p => !p.IsDeleted);

            

        }

    }

}




