using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartGarcom.Migrations
{
    public partial class AddOrderToken_And_Remove_Unecessary_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "OrderCards");

            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "OrderCards");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "OrderCards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "OrderCards",
                nullable: true);
        }
    }
}
