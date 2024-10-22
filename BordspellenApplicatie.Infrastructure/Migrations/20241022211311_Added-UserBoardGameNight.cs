using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BordspellenApplicatie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserBoardGameNight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSignedUp",
                schema: "Bordspellenavond",
                table: "BoardGameNights",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserBoardGameNights",
                schema: "Bordspellenavond",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoardGameNightId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBoardGameNights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBoardGameNights_BoardGameNights_BoardGameNightId",
                        column: x => x.BoardGameNightId,
                        principalSchema: "Bordspellenavond",
                        principalTable: "BoardGameNights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBoardGameNights_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Bordspellenavond",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBoardGameNights_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "UserBoardGameNights",
                column: "BoardGameNightId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBoardGameNights_UserId",
                schema: "Bordspellenavond",
                table: "UserBoardGameNights",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBoardGameNights",
                schema: "Bordspellenavond");

            migrationBuilder.DropColumn(
                name: "IsSignedUp",
                schema: "Bordspellenavond",
                table: "BoardGameNights");
        }
    }
}
