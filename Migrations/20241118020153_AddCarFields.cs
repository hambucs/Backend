using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddCarFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableFrom",
                table: "Cars",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableTo",
                table: "Cars",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cars",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Cars",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Cars",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Mileage",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "AvailableFrom", "AvailableTo", "Description", "IsAvailable", "Location", "Mileage" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, 0 });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "RentalEnd", "RentalStart" },
                values: new object[] { new DateTime(2024, 11, 20, 3, 1, 52, 757, DateTimeKind.Local).AddTicks(291), new DateTime(2024, 11, 18, 3, 1, 52, 757, DateTimeKind.Local).AddTicks(240) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableFrom",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "AvailableTo",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Mileage",
                table: "Cars");

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "RentalEnd", "RentalStart" },
                values: new object[] { new DateTime(2024, 11, 13, 21, 24, 23, 923, DateTimeKind.Local).AddTicks(1775), new DateTime(2024, 11, 11, 21, 24, 23, 923, DateTimeKind.Local).AddTicks(1726) });
        }
    }
}
