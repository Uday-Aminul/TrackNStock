using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackNStock.Api.Migrations
{
    /// <inheritdoc />
    public partial class Salesdataseeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "OrderId", "Quantity", "SalesDate", "UnitPrice" },
                values: new object[] { 6601, 7701, 1, new DateTime(2025, 10, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), 110 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: 6601);
        }
    }
}
