using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusForCommission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommissionStatusId",
                table: "CommissionHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CommissionStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommissionHistory_CommissionStatusId",
                table: "CommissionHistory",
                column: "CommissionStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionHistory_CommissionStatus_CommissionStatusId",
                table: "CommissionHistory",
                column: "CommissionStatusId",
                principalTable: "CommissionStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommissionHistory_CommissionStatus_CommissionStatusId",
                table: "CommissionHistory");

            migrationBuilder.DropTable(
                name: "CommissionStatus");

            migrationBuilder.DropIndex(
                name: "IX_CommissionHistory_CommissionStatusId",
                table: "CommissionHistory");

            migrationBuilder.DropColumn(
                name: "CommissionStatusId",
                table: "CommissionHistory");
        }
    }
}
