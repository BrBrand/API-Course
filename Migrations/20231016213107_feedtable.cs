using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace next_generation.Migrations
{
    /// <inheritdoc />
    public partial class feedtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NextGenUser",
                columns: new[] { "Id", "CreationDate", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 16, 15, 31, 7, 772, DateTimeKind.Local).AddTicks(8752), "john.doe@example.com", "John", "Doe", "secretpassword1" },
                    { 2, new DateTime(2023, 10, 16, 15, 31, 7, 772, DateTimeKind.Local).AddTicks(8764), "jane.smith@example.com", "Jane", "Smith", "secretpassword2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NextGenUser",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NextGenUser",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
