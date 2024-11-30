using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Cars",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "AvailableFrom", "AvailableTo", "DailyRate", "Description", "ImagePath", "Location", "Mileage" },
                values: new object[] { new DateTime(2024, 11, 21, 18, 22, 58, 482, DateTimeKind.Local).AddTicks(7500), new DateTime(2024, 12, 20, 18, 22, 58, 482, DateTimeKind.Local).AddTicks(7547), 50m, "A reliable family car, well-maintained.", null, "Budapest, Hungary", 25000 });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "RentalEnd", "RentalStart" },
                values: new object[] { new DateTime(2024, 11, 22, 18, 22, 58, 482, DateTimeKind.Local).AddTicks(7576), new DateTime(2024, 11, 20, 18, 22, 58, 482, DateTimeKind.Local).AddTicks(7574) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Cars");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "AvailableFrom", "AvailableTo", "DailyRate", "Description", "Location", "Mileage" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30m, null, null, 0 });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "RentalEnd", "RentalStart" },
                values: new object[] { new DateTime(2024, 11, 20, 3, 1, 52, 757, DateTimeKind.Local).AddTicks(291), new DateTime(2024, 11, 18, 3, 1, 52, 757, DateTimeKind.Local).AddTicks(240) });
        }
    }
}
