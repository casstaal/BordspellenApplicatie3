using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BordspellenApplicatie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedBoardGameNight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "Bordspellenavond",
                table: "BoardGameNights",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                schema: "Bordspellenavond",
                table: "BoardGameNights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Number",
                schema: "Bordspellenavond",
                table: "BoardGameNights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                schema: "Bordspellenavond",
                table: "BoardGameNights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Is18Plus",
                schema: "Bordspellenavond",
                table: "BoardGameNights",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                schema: "Bordspellenavond",
                table: "BoardGameNights");

            migrationBuilder.DropColumn(
                name: "Address_Number",
                schema: "Bordspellenavond",
                table: "BoardGameNights");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                schema: "Bordspellenavond",
                table: "BoardGameNights");

            migrationBuilder.DropColumn(
                name: "Is18Plus",
                schema: "Bordspellenavond",
                table: "BoardGameNights");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "Bordspellenavond",
                table: "BoardGameNights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
