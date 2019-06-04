using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartGarcom.Migrations
{
    public partial class AddCPF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserTUserId",
                table: "OrderCards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderCards_UserTUserId",
                table: "OrderCards",
                column: "UserTUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCards_TUsers_UserTUserId",
                table: "OrderCards",
                column: "UserTUserId",
                principalTable: "TUsers",
                principalColumn: "TUserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCards_TUsers_UserTUserId",
                table: "OrderCards");

            migrationBuilder.DropIndex(
                name: "IX_OrderCards_UserTUserId",
                table: "OrderCards");

            migrationBuilder.DropColumn(
                name: "UserTUserId",
                table: "OrderCards");
        }
    }
}
