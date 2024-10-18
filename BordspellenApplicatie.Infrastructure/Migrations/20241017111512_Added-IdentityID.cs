using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BordspellenApplicatie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIdentityID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDate",
                schema: "Bordspellenavond",
                table: "User",
                newName: "DateOfBirth");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "Bordspellenavond",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                schema: "Bordspellenavond",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                schema: "Bordspellenavond",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityId",
                schema: "Bordspellenavond",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Role",
                schema: "Bordspellenavond",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                schema: "Bordspellenavond",
                table: "User",
                newName: "BirthDate");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "Bordspellenavond",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
