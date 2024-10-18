using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BordspellenApplicatie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DrinksAlcohol",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HasNutAllergy",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsLactoseIntolerant",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsVegetarian",
                schema: "Bordspellenavond",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "Bordspellenavond",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                schema: "Bordspellenavond",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "DrinksAlcohol",
                schema: "Bordspellenavond",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "Bordspellenavond",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                schema: "Bordspellenavond",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasNutAllergy",
                schema: "Bordspellenavond",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                schema: "Bordspellenavond",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLactoseIntolerant",
                schema: "Bordspellenavond",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVegetarian",
                schema: "Bordspellenavond",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "Bordspellenavond",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
