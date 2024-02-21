using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateArtworkAndPurchaseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BuyDate",
                table: "Purchases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "BuyPrice",
                table: "Purchases",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "RequestDescription",
                table: "CommissionRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoldNumber",
                table: "Artworks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyDate",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "BuyPrice",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "RequestDescription",
                table: "CommissionRequests");

            migrationBuilder.DropColumn(
                name: "SoldNumber",
                table: "Artworks");
        }
    }
}
