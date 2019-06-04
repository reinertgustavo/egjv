using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartGarcom.Migrations
{
    public partial class AddOrderToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "orderCardToken",
                table: "OrderCards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "orderCardToken",
                table: "OrderCards");
        }
    }
}
