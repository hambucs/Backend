using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHashToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add the PasswordHash column with the appropriate type and charset
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            // Add the Role column with a default value of "User"
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "User")
                .Annotation("MySql:CharSet", "utf8mb4");

            // Optional: Update existing data in Users table
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "PasswordHash", "Role" },
                values: new object[] { "Tb1eSRR7UQLuJzGsA90Nt97MO4cVw988Hz3cYty8+G0=", "User" });
            
            // Modify Cars table columns
            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Cars",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Make",
                table: "Cars",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            // Optional: Update Rental dates if needed
            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "RentalEnd", "RentalStart" },
                values: new object[] { new DateTime(2024, 11, 5, 19, 48, 5, 502, DateTimeKind.Local).AddTicks(2575), new DateTime(2024, 11, 3, 19, 48, 5, 502, DateTimeKind.Local).AddTicks(2522) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the PasswordHash and Role columns
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            // Revert Cars table columns to original types
            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Cars",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Make",
                table: "Cars",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            // Update Rental dates back to original
            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "RentalEnd", "RentalStart" },
                values: new object[] { new DateTime(2024, 11, 4, 22, 31, 29, 147, DateTimeKind.Local).AddTicks(8731), new DateTime(2024, 11, 2, 22, 31, 29, 147, DateTimeKind.Local).AddTicks(8680) });
        }
    }
}
