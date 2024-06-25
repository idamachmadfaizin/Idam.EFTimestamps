﻿// <auto-generated />
using System;
using Idam.EFTimestamps.Sample.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Idam.Libs.EF.Sample.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240621234349_CreateDtsTable")]
    partial class CreateDtsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Idam.Libs.EF.Sample.Models.Entity.Dt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Dts");
                });

            modelBuilder.Entity("Idam.Libs.EF.Sample.Models.Entity.Unix", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("CreatedAt")
                        .HasColumnType("bigint");

                    b.Property<long?>("DeletedAt")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<long>("UpdatedAt")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Unixs");
                });
#pragma warning restore 612, 618
        }
    }
}