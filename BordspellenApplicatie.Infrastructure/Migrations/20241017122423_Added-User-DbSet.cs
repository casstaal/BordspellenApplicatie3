using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BordspellenApplicatie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGameNights_User_OrganizerId",
                schema: "Bordspellenavond",
                table: "BoardGameNights");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Addresses_AddressId",
                schema: "Bordspellenavond",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_BoardGameNights_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                schema: "Bordspellenavond",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "Bordspellenavond",
                newName: "Users",
                newSchema: "Bordspellenavond");

            migrationBuilder.RenameIndex(
                name: "IX_User_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "Users",
                newName: "IX_Users_BoardGameNightId");

            migrationBuilder.RenameIndex(
                name: "IX_User_AddressId",
                schema: "Bordspellenavond",
                table: "Users",
                newName: "IX_Users_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "Bordspellenavond",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGameNights_Users_OrganizerId",
                schema: "Bordspellenavond",
                table: "BoardGameNights",
                column: "OrganizerId",
                principalSchema: "Bordspellenavond",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_AddressId",
                schema: "Bordspellenavond",
                table: "Users",
                column: "AddressId",
                principalSchema: "Bordspellenavond",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_BoardGameNights_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "Users",
                column: "BoardGameNightId",
                principalSchema: "Bordspellenavond",
                principalTable: "BoardGameNights",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGameNights_Users_OrganizerId",
                schema: "Bordspellenavond",
                table: "BoardGameNights");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_AddressId",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_BoardGameNights_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Bordspellenavond",
                newName: "User",
                newSchema: "Bordspellenavond");

            migrationBuilder.RenameIndex(
                name: "IX_Users_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "User",
                newName: "IX_User_BoardGameNightId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_AddressId",
                schema: "Bordspellenavond",
                table: "User",
                newName: "IX_User_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "Bordspellenavond",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGameNights_User_OrganizerId",
                schema: "Bordspellenavond",
                table: "BoardGameNights",
                column: "OrganizerId",
                principalSchema: "Bordspellenavond",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Addresses_AddressId",
                schema: "Bordspellenavond",
                table: "User",
                column: "AddressId",
                principalSchema: "Bordspellenavond",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_BoardGameNights_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "User",
                column: "BoardGameNightId",
                principalSchema: "Bordspellenavond",
                principalTable: "BoardGameNights",
                principalColumn: "Id");
        }
    }
}
