﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpajzManager.Api.DbContexts;

#nullable disable

namespace SpajzManager.Api.Migrations
{
    [DbContext(typeof(SpajzManagerContext))]
    [Migration("20250316202437_AddCreatedAtPropertyToItem")]
    partial class AddCreatedAtPropertyToItem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("SpajzManager.Api.Entities.Household", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Households");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Szülői ház",
                            Name = "Városlődi kecó"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Albérlet",
                            Name = "Palotai kégli"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Nyaraló",
                            Name = "Györöki kisház"
                        });
                });

            modelBuilder.Entity("SpajzManager.Api.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("HouseholdId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("Unit")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("HouseholdId");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "gyümölcs",
                            HouseholdId = 1,
                            Name = "Alma",
                            Quantity = 1m,
                            Unit = 0
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "egyenesen a tehénből",
                            HouseholdId = 1,
                            Name = "Tej",
                            Quantity = 1m,
                            Unit = 0
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "utlimate zöldség",
                            HouseholdId = 1,
                            Name = "Brokkoli",
                            Quantity = 1m,
                            Unit = 0
                        });
                });

            modelBuilder.Entity("SpajzManager.Api.Entities.Item", b =>
                {
                    b.HasOne("SpajzManager.Api.Entities.Household", "Household")
                        .WithMany("Items")
                        .HasForeignKey("HouseholdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Household");
                });

            modelBuilder.Entity("SpajzManager.Api.Entities.Household", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
