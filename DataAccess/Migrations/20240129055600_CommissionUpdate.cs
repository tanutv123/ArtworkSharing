using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CommissionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgressImageId",
                table: "CommissionHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommissionHistory_ProgressImageId",
                table: "CommissionHistory",
                column: "ProgressImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionHistory_Images_ProgressImageId",
                table: "CommissionHistory",
                column: "ProgressImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommissionHistory_Images_ProgressImageId",
                table: "CommissionHistory");

            migrationBuilder.DropIndex(
                name: "IX_CommissionHistory_ProgressImageId",
                table: "CommissionHistory");

            migrationBuilder.DropColumn(
                name: "ProgressImageId",
                table: "CommissionHistory");
        }
    }
}
