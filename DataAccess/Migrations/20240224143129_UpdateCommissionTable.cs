using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommissionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActualPrice",
                table: "CommissionRequests",
                newName: "MinPrice");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "CommissionRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxPrice",
                table: "CommissionRequests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "NotAcceptedReason",
                table: "CommissionRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommissionRequests_GenreId",
                table: "CommissionRequests",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionRequests_Genres_GenreId",
                table: "CommissionRequests",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommissionRequests_Genres_GenreId",
                table: "CommissionRequests");

            migrationBuilder.DropIndex(
                name: "IX_CommissionRequests_GenreId",
                table: "CommissionRequests");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "CommissionRequests");

            migrationBuilder.DropColumn(
                name: "MaxPrice",
                table: "CommissionRequests");

            migrationBuilder.DropColumn(
                name: "NotAcceptedReason",
                table: "CommissionRequests");

            migrationBuilder.RenameColumn(
                name: "MinPrice",
                table: "CommissionRequests",
                newName: "ActualPrice");
        }
    }
}
