using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCommissionImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommissionImage_CommissionRequests_CommisionHistoryId",
                table: "CommissionImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommissionImage",
                table: "CommissionImage");

            migrationBuilder.RenameTable(
                name: "CommissionImage",
                newName: "CommissionImages");

            migrationBuilder.RenameIndex(
                name: "IX_CommissionImage_CommisionHistoryId",
                table: "CommissionImages",
                newName: "IX_CommissionImages_CommisionHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommissionImages",
                table: "CommissionImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionImages_CommissionRequests_CommisionHistoryId",
                table: "CommissionImages",
                column: "CommisionHistoryId",
                principalTable: "CommissionRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommissionImages_CommissionRequests_CommisionHistoryId",
                table: "CommissionImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommissionImages",
                table: "CommissionImages");

            migrationBuilder.RenameTable(
                name: "CommissionImages",
                newName: "CommissionImage");

            migrationBuilder.RenameIndex(
                name: "IX_CommissionImages_CommisionHistoryId",
                table: "CommissionImage",
                newName: "IX_CommissionImage_CommisionHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommissionImage",
                table: "CommissionImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionImage_CommissionRequests_CommisionHistoryId",
                table: "CommissionImage",
                column: "CommisionHistoryId",
                principalTable: "CommissionRequests",
                principalColumn: "Id");
        }
    }
}
