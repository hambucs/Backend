﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Szakdoga.Backend.Data;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(CarRentalContext))]
    partial class CarRentalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Szakdoga.Backend.Models.Car", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("AvailableFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("AvailableTo")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("DailyRate")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Location")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Mileage")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("OwnerID")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            AvailableFrom = new DateTime(2024, 11, 21, 18, 22, 58, 482, DateTimeKind.Local).AddTicks(7500),
                            AvailableTo = new DateTime(2024, 12, 20, 18, 22, 58, 482, DateTimeKind.Local).AddTicks(7547),
                            DailyRate = 50m,
                            Description = "A reliable family car, well-maintained.",
                            IsAvailable = true,
                            Location = "Budapest, Hungary",
                            Make = "Toyota",
                            Mileage = 25000,
                            Model = "Corolla",
                            OwnerID = 1,
                            Year = 2020
                        });
                });

            modelBuilder.Entity("Szakdoga.Backend.Models.Rental", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("CarID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RentalEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("RentalStart")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("RenterID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CarID");

                    b.HasIndex("RenterID");

                    b.ToTable("Rentals");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CarID = 1,
                            RentalEnd = new DateTime(2024, 11, 22, 18, 22, 58, 482, DateTimeKind.Local).AddTicks(7576),
                            RentalStart = new DateTime(2024, 11, 20, 18, 22, 58, 482, DateTimeKind.Local).AddTicks(7574),
                            RenterID = 1
                        });
                });

            modelBuilder.Entity("Szakdoga.Backend.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Email = "admin@admin.com",
                            PasswordHash = "Tb1eSRR7UQLuJzGsA90Nt97MO4cVw988Hz3cYty8+G0=",
                            Role = "User",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("Szakdoga.Backend.Models.Car", b =>
                {
                    b.HasOne("Szakdoga.Backend.Models.User", "Owner")
                        .WithMany("Cars")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Szakdoga.Backend.Models.Rental", b =>
                {
                    b.HasOne("Szakdoga.Backend.Models.Car", "Car")
                        .WithMany("Rentals")
                        .HasForeignKey("CarID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Szakdoga.Backend.Models.User", "Renter")
                        .WithMany("Rentals")
                        .HasForeignKey("RenterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Renter");
                });

            modelBuilder.Entity("Szakdoga.Backend.Models.Car", b =>
                {
                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("Szakdoga.Backend.Models.User", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Rentals");
                });
#pragma warning restore 612, 618
        }
    }
}