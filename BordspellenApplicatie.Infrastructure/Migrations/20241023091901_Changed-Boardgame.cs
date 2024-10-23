using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BordspellenApplicatie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedBoardgame : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "Bordspellenavond",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
