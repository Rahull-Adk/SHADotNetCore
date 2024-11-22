﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SHADotNetCore.MiniKBZPay.Models;

#nullable disable

namespace SHADotNetCore.MiniKBZPay.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20241122050751_Last-Update")]
partial class LastUpdate
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "7.0.0")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("SHADotNetCore.MiniKBZPay.Model.UserModel", b =>
            {
                b.Property<int>("UserId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                b.Property<int>("Balance")
                    .HasColumnType("int");

                b.Property<string>("FullName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("MobileNumber")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("PIN")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("UserId");

                b.ToTable("Users");
            });

        modelBuilder.Entity("SHADotNetCore.MiniKBZPay.Models.Receipt", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<decimal>("Amount")
                    .HasColumnType("decimal(18,2)");

                b.Property<string>("FromMobileNumber")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Note")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ToMobileNumber")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("TransactionDate")
                    .HasColumnType("datetime2");

                b.Property<int>("TransactionType")
                    .HasColumnType("int");

                b.Property<int?>("UserModelUserId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("UserModelUserId");

                b.ToTable("Receipt");
            });

        modelBuilder.Entity("SHADotNetCore.MiniKBZPay.Models.Receipt", b =>
            {
                b.HasOne("SHADotNetCore.MiniKBZPay.Model.UserModel", null)
                    .WithMany("receipts")
                    .HasForeignKey("UserModelUserId");
            });

        modelBuilder.Entity("SHADotNetCore.MiniKBZPay.Model.UserModel", b =>
            {
                b.Navigation("receipts");
            });
#pragma warning restore 612, 618
    }
}
