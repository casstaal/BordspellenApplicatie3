using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BordspellenApplicatie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Bordspellenavond");

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "Bordspellenavond",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardGameNights",
                schema: "Bordspellenavond",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizerId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false),
                    Food = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLactoseIntoleranceFriendly = table.Column<bool>(type: "bit", nullable: false),
                    IsVegetarianFriendly = table.Column<bool>(type: "bit", nullable: false),
                    IsNutAllergyFriendly = table.Column<bool>(type: "bit", nullable: false),
                    IsNonAlcoholicFriendly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameNights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                schema: "Bordspellenavond",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    Is18Plus = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    BoardGameNightId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_BoardGameNights_BoardGameNightId",
                        column: x => x.BoardGameNightId,
                        principalSchema: "Bordspellenavond",
                        principalTable: "BoardGameNights",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Bordspellenavond",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLactoseIntolerant = table.Column<bool>(type: "bit", nullable: false),
                    IsVegetarian = table.Column<bool>(type: "bit", nullable: false),
                    HasNutAllergy = table.Column<bool>(type: "bit", nullable: false),
                    DrinksAlcohol = table.Column<bool>(type: "bit", nullable: false),
                    BoardGameNightId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "Bordspellenavond",
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_BoardGameNights_BoardGameNightId",
                        column: x => x.BoardGameNightId,
                        principalSchema: "Bordspellenavond",
                        principalTable: "BoardGameNights",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameNights_OrganizerId",
                schema: "Bordspellenavond",
                table: "BoardGameNights",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "Games",
                column: "BoardGameNightId");

            migrationBuilder.CreateIndex(
                name: "IX_User_AddressId",
                schema: "Bordspellenavond",
                table: "User",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_User_BoardGameNightId",
                schema: "Bordspellenavond",
                table: "User",
                column: "BoardGameNightId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGameNights_User_OrganizerId",
                schema: "Bordspellenavond",
                table: "BoardGameNights",
                column: "OrganizerId",
                principalSchema: "Bordspellenavond",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGameNights_User_OrganizerId",
                schema: "Bordspellenavond",
                table: "BoardGameNights");

            migrationBuilder.DropTable(
                name: "Games",
                schema: "Bordspellenavond");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Bordspellenavond");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "Bordspellenavond");

            migrationBuilder.DropTable(
                name: "BoardGameNights",
                schema: "Bordspellenavond");
        }
    }
}
