using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BordspellenApplicatie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDBContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_BoardGameNights_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBoardGameNights_BoardGameNights_BoardGameNightId1",
                schema: "Bordspellenavond",
                table: "UserBoardGameNights");

            migrationBuilder.DropIndex(
                name: "IX_UserBoardGameNights_BoardGameNightId1",
                schema: "Bordspellenavond",
                table: "UserBoardGameNights");

            migrationBuilder.DropColumn(
                name: "BoardGameNightId1",
                schema: "Bordspellenavond",
                table: "UserBoardGameNights");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_BoardGameNights_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "Games",
                column: "BoardGameNightId",
                principalSchema: "Bordspellenavond",
                principalTable: "BoardGameNights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_BoardGameNights_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "BoardGameNightId1",
                schema: "Bordspellenavond",
                table: "UserBoardGameNights",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBoardGameNights_BoardGameNightId1",
                schema: "Bordspellenavond",
                table: "UserBoardGameNights",
                column: "BoardGameNightId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_BoardGameNights_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "Games",
                column: "BoardGameNightId",
                principalSchema: "Bordspellenavond",
                principalTable: "BoardGameNights",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBoardGameNights_BoardGameNights_BoardGameNightId1",
                schema: "Bordspellenavond",
                table: "UserBoardGameNights",
                column: "BoardGameNightId1",
                principalSchema: "Bordspellenavond",
                principalTable: "BoardGameNights",
                principalColumn: "Id");
        }
    }
}
