﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpajzManager.Api.DbContexts;

#nullable disable

namespace SpajzManager.Api.Migrations
{
    [DbContext(typeof(SpajzManagerContext))]
    [Migration("20250304195357_QuantityUnitImplementation")]
    partial class QuantityUnitImplementation
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

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("HouseholdId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Unit")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("HouseholdId");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "gyümölcs",
                            HouseholdId = 1,
                            Name = "Alma",
                            Unit = 0
                        },
                        new
                        {
                            Id = 2,
                            Description = "egyenesen a tehénből",
                            HouseholdId = 1,
                            Name = "Tej",
                            Unit = 0
                        },
                        new
                        {
                            Id = 3,
                            Description = "utlimate zöldség",
                            HouseholdId = 1,
                            Name = "Brokkoli",
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
