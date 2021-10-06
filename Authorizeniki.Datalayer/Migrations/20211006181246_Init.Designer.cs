﻿// <auto-generated />
using System;
using Authorizeniki.Datalayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Authorizeniki.Datalayer.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20211006181246_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Authorizeniki.Datalayer.Tables.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EventTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RfId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Authorizeniki.Datalayer.Tables.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b29f65e7-43b9-4a14-9ab2-ddca2312f2e3"),
                            Name = "admin",
                            Salary = 999999999m
                        },
                        new
                        {
                            Id = new Guid("57eb14fb-cfb9-43b6-869d-28bb06e57540"),
                            Name = "manager",
                            Salary = 1m
                        });
                });

            modelBuilder.Entity("Authorizeniki.Datalayer.Tables.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique()
                        .HasFilter("[Login] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("869ab7ac-227d-4f45-b6b7-30455bd86a83"),
                            FirstName = "Админ",
                            LastName = "Фсея Руси",
                            Login = "admin",
                            Password = "admin",
                            RoleId = new Guid("b29f65e7-43b9-4a14-9ab2-ddca2312f2e3"),
                            Surname = "Админович"
                        },
                        new
                        {
                            Id = new Guid("1af8d4a8-0577-4f7b-a917-2083cf3590d7"),
                            FirstName = "Менеджер",
                            LastName = "Фсея менеджеров.рф",
                            Login = "manager",
                            Password = "manager",
                            RoleId = new Guid("57eb14fb-cfb9-43b6-869d-28bb06e57540"),
                            Surname = "Менеджерович"
                        });
                });

            modelBuilder.Entity("Authorizeniki.Datalayer.Tables.Event", b =>
                {
                    b.HasOne("Authorizeniki.Datalayer.Tables.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Authorizeniki.Datalayer.Tables.User", b =>
                {
                    b.HasOne("Authorizeniki.Datalayer.Tables.Role", "UserRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });
#pragma warning restore 612, 618
        }
    }
}