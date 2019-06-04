using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartGarcom.Migrations
{
    public partial class IncludeCPFonUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "TUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "OrderCards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "OrderCards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "TUsers");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "OrderCards");

            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "OrderCards");
        }
    }
}
