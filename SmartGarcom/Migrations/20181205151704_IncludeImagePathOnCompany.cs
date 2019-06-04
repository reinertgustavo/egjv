using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartGarcom.Migrations
{
    public partial class IncludeImagePathOnCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Companies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Companies");
        }
    }
}
