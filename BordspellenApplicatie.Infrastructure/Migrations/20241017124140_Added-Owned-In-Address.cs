using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BordspellenApplicatie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedOwnedInAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_AddressId",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "Bordspellenavond");

            migrationBuilder.DropIndex(
                name: "IX_Users_AddressId",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                schema: "Bordspellenavond",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Number",
                schema: "Bordspellenavond",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                schema: "Bordspellenavond",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Address_Number",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                schema: "Bordspellenavond",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "Bordspellenavond",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                schema: "Bordspellenavond",
                table: "Users",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_AddressId",
                schema: "Bordspellenavond",
                table: "Users",
                column: "AddressId",
                principalSchema: "Bordspellenavond",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
