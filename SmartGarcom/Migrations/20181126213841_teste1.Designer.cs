﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartGarcom.Models;

namespace SmartGarcom.Migrations
{
    [DbContext(typeof(Banco))]
    [Migration("20181126213841_teste1")]
    partial class teste1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmartGarcom.Models.Company", b =>
                {
                    b.Property<long>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Cnpj")
                        .IsRequired();

                    b.Property<string>("CommercialNumber");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Neighborhood");

                    b.Property<string>("SocialName");

                    b.Property<string>("State");

                    b.Property<string>("StreetAddress");

                    b.Property<string>("StreetNumber");

                    b.Property<string>("ZipCode")
                        .IsRequired();

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("SmartGarcom.Models.Order", b =>
                {
                    b.Property<long>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("OrderCardId");

                    b.Property<long?>("StatusID");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderCardId");

                    b.HasIndex("StatusID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SmartGarcom.Models.OrderCard", b =>
                {
                    b.Property<long>("OrderCardId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CompanyId");

                    b.Property<long?>("TableId");

                    b.Property<long?>("UserTUserId");

                    b.Property<string>("orderCardToken");

                    b.HasKey("OrderCardId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("TableId");

                    b.HasIndex("UserTUserId");

                    b.ToTable("OrderCards");
                });

            modelBuilder.Entity("SmartGarcom.Models.OrderProduct", b =>
                {
                    b.Property<long>("OrderProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("OrderId");

                    b.Property<long?>("ProductId");

                    b.Property<long>("Quantity");

                    b.HasKey("OrderProductId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("SmartGarcom.Models.Product", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CompanyId");

                    b.Property<string>("Description");

                    b.Property<string>("ImagePath");

                    b.Property<string>("IsActive");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<long?>("ProductCategoryId");

                    b.HasKey("ProductId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SmartGarcom.Models.ProductCategory", b =>
                {
                    b.Property<long>("ProductCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CompanyId");

                    b.Property<string>("Description");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name");

                    b.HasKey("ProductCategoryId");

                    b.HasIndex("CompanyId");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("SmartGarcom.Models.Role", b =>
                {
                    b.Property<long>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SmartGarcom.Models.Status", b =>
                {
                    b.Property<long>("StatusID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("StatusID");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("SmartGarcom.Models.Table", b =>
                {
                    b.Property<long>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CompanyId");

                    b.Property<string>("Number");

                    b.Property<string>("QRCode");

                    b.HasKey("TableId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("SmartGarcom.Models.TUser", b =>
                {
                    b.Property<long>("TUserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthToken");

                    b.Property<DateTime>("Birthdate");

                    b.Property<string>("CPF");

                    b.Property<long?>("CompanyId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password");

                    b.Property<long?>("RoleId");

                    b.HasKey("TUserId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("RoleId");

                    b.ToTable("TUsers");
                });

            modelBuilder.Entity("SmartGarcom.Models.Order", b =>
                {
                    b.HasOne("SmartGarcom.Models.OrderCard", "OrderCard")
                        .WithMany()
                        .HasForeignKey("OrderCardId");

                    b.HasOne("SmartGarcom.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusID");
                });

            modelBuilder.Entity("SmartGarcom.Models.OrderCard", b =>
                {
                    b.HasOne("SmartGarcom.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("SmartGarcom.Models.Table", "Table")
                        .WithMany()
                        .HasForeignKey("TableId");

                    b.HasOne("SmartGarcom.Models.TUser", "User")
                        .WithMany()
                        .HasForeignKey("UserTUserId");
                });

            modelBuilder.Entity("SmartGarcom.Models.OrderProduct", b =>
                {
                    b.HasOne("SmartGarcom.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("SmartGarcom.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("SmartGarcom.Models.Product", b =>
                {
                    b.HasOne("SmartGarcom.Models.Company", "Company")
                        .WithMany("Products")
                        .HasForeignKey("CompanyId");

                    b.HasOne("SmartGarcom.Models.ProductCategory", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("ProductCategoryId");
                });

            modelBuilder.Entity("SmartGarcom.Models.ProductCategory", b =>
                {
                    b.HasOne("SmartGarcom.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("SmartGarcom.Models.Table", b =>
                {
                    b.HasOne("SmartGarcom.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("SmartGarcom.Models.TUser", b =>
                {
                    b.HasOne("SmartGarcom.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("SmartGarcom.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });
#pragma warning restore 612, 618
        }
    }
}
