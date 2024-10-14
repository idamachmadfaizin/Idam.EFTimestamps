﻿// <auto-generated />
using System;
using Idam.EFTimestamps.Sample.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Idam.EFTimestamps.Sample.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20241013165737_CreateDtsTable")]
    partial class CreateDtsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Idam.EFTimestamps.Sample.Models.Entity.Dt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(191)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Dts");
                });

            modelBuilder.Entity("Idam.EFTimestamps.Sample.Models.Entity.Unix", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("CreatedAt")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("DeletedAt")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(191)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("TEXT");

                    b.Property<long>("UpdatedAt")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Unixs");
                });
#pragma warning restore 612, 618
        }
    }
}
