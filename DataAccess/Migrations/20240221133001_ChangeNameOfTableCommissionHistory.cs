using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameOfTableCommissionHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommissionImage_CommissionHistory_CommisionHistoryId",
                table: "CommissionImage");

            migrationBuilder.DropTable(
                name: "CommissionHistory");

            migrationBuilder.CreateTable(
                name: "CommissionRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActualPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    CommissionStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommissionRequests_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CommissionRequests_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommissionRequests_CommissionStatus_CommissionStatusId",
                        column: x => x.CommissionStatusId,
                        principalTable: "CommissionStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommissionRequests_CommissionStatusId",
                table: "CommissionRequests",
                column: "CommissionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionRequests_ReceiverId",
                table: "CommissionRequests",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionRequests_SenderId",
                table: "CommissionRequests",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionImage_CommissionRequests_CommisionHistoryId",
                table: "CommissionImage",
                column: "CommisionHistoryId",
                principalTable: "CommissionRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommissionImage_CommissionRequests_CommisionHistoryId",
                table: "CommissionImage");

            migrationBuilder.DropTable(
                name: "CommissionRequests");

            migrationBuilder.CreateTable(
                name: "CommissionHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommissionStatusId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ActualPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommissionHistory_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CommissionHistory_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommissionHistory_CommissionStatus_CommissionStatusId",
                        column: x => x.CommissionStatusId,
                        principalTable: "CommissionStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommissionHistory_CommissionStatusId",
                table: "CommissionHistory",
                column: "CommissionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionHistory_ReceiverId",
                table: "CommissionHistory",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionHistory_SenderId",
                table: "CommissionHistory",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionImage_CommissionHistory_CommisionHistoryId",
                table: "CommissionImage",
                column: "CommisionHistoryId",
                principalTable: "CommissionHistory",
                principalColumn: "Id");
        }
    }
}
