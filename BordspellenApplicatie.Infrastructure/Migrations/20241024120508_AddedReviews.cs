using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BordspellenApplicatie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_BoardGameNights_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BoardGameNightId",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "BoardGameNightId1",
                schema: "Bordspellenavond",
                table: "UserBoardGameNights",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "Bordspellenavond",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "Bordspellenavond",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    TextReview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardGameNightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_BoardGameNights_BoardGameNightId",
                        column: x => x.BoardGameNightId,
                        principalSchema: "Bordspellenavond",
                        principalTable: "BoardGameNights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "Bordspellenavond",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBoardGameNights_BoardGameNightId1",
                schema: "Bordspellenavond",
                table: "UserBoardGameNights",
                column: "BoardGameNightId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AuthorId",
                schema: "Bordspellenavond",
                table: "Reviews",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "Reviews",
                column: "BoardGameNightId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBoardGameNights_BoardGameNights_BoardGameNightId1",
                schema: "Bordspellenavond",
                table: "UserBoardGameNights",
                column: "BoardGameNightId1",
                principalSchema: "Bordspellenavond",
                principalTable: "BoardGameNights",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBoardGameNights_BoardGameNights_BoardGameNightId1",
                schema: "Bordspellenavond",
                table: "UserBoardGameNights");

            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "Bordspellenavond");

            migrationBuilder.DropIndex(
                name: "IX_UserBoardGameNights_BoardGameNightId1",
                schema: "Bordspellenavond",
                table: "UserBoardGameNights");

            migrationBuilder.DropColumn(
                name: "BoardGameNightId1",
                schema: "Bordspellenavond",
                table: "UserBoardGameNights");

            migrationBuilder.AddColumn<int>(
                name: "BoardGameNightId",
                schema: "Bordspellenavond",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "Bordspellenavond",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "Users",
                column: "BoardGameNightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_BoardGameNights_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "Users",
                column: "BoardGameNightId",
                principalSchema: "Bordspellenavond",
                principalTable: "BoardGameNights",
                principalColumn: "Id");
        }
    }
}
