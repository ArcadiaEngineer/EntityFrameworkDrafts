using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCore.Console.Migrations
{
    public partial class ownedEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Students");
        }
    }
}
