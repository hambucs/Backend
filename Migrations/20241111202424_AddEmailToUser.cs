using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "RentalEnd", "RentalStart" },
                values: new object[] { new DateTime(2024, 11, 13, 21, 24, 23, 923, DateTimeKind.Local).AddTicks(1775), new DateTime(2024, 11, 11, 21, 24, 23, 923, DateTimeKind.Local).AddTicks(1726) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Email",
                value: "admin@admin.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "RentalEnd", "RentalStart" },
                values: new object[] { new DateTime(2024, 11, 5, 19, 48, 5, 502, DateTimeKind.Local).AddTicks(2575), new DateTime(2024, 11, 3, 19, 48, 5, 502, DateTimeKind.Local).AddTicks(2522) });
        }
    }
}
