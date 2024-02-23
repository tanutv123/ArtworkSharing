using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCommissionImageTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommissionImages_CommissionRequests_CommisionHistoryId",
                table: "CommissionImages");

            migrationBuilder.DropIndex(
                name: "IX_CommissionImages_CommisionHistoryId",
                table: "CommissionImages");

            migrationBuilder.DropColumn(
                name: "CommisionHistoryId",
                table: "CommissionImages");

            migrationBuilder.RenameColumn(
                name: "CommissionHistoryId",
                table: "CommissionImages",
                newName: "CommissionRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionImages_CommissionRequestId",
                table: "CommissionImages",
                column: "CommissionRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionImages_CommissionRequests_CommissionRequestId",
                table: "CommissionImages",
                column: "CommissionRequestId",
                principalTable: "CommissionRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommissionImages_CommissionRequests_CommissionRequestId",
                table: "CommissionImages");

            migrationBuilder.DropIndex(
                name: "IX_CommissionImages_CommissionRequestId",
                table: "CommissionImages");

            migrationBuilder.RenameColumn(
                name: "CommissionRequestId",
                table: "CommissionImages",
                newName: "CommissionHistoryId");

            migrationBuilder.AddColumn<int>(
                name: "CommisionHistoryId",
                table: "CommissionImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommissionImages_CommisionHistoryId",
                table: "CommissionImages",
                column: "CommisionHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionImages_CommissionRequests_CommisionHistoryId",
                table: "CommissionImages",
                column: "CommisionHistoryId",
                principalTable: "CommissionRequests",
                principalColumn: "Id");
        }
    }
}
