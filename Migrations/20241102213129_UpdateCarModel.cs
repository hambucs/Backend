using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCarModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "RentalEnd", "RentalStart" },
                values: new object[] { new DateTime(2024, 11, 4, 22, 31, 29, 147, DateTimeKind.Local).AddTicks(8731), new DateTime(2024, 11, 2, 22, 31, 29, 147, DateTimeKind.Local).AddTicks(8680) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "RentalEnd", "RentalStart" },
                values: new object[] { new DateTime(2024, 11, 4, 21, 10, 10, 269, DateTimeKind.Local).AddTicks(2311), new DateTime(2024, 11, 2, 21, 10, 10, 269, DateTimeKind.Local).AddTicks(2261) });
        }
    }
}
