using Microsoft.EntityFrameworkCore.Migrations;

namespace EGJV.Migrations
{
    public partial class up2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Responsavel",
                table: "Tickets",
                newName: "EmailSolicitante");

            migrationBuilder.AddColumn<long>(
                name: "TUserId",
                table: "Tickets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TUserId",
                table: "Tickets",
                column: "TUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TUsers_TUserId",
                table: "Tickets",
                column: "TUserId",
                principalTable: "TUsers",
                principalColumn: "TUserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TUsers_TUserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TUserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TUserId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "EmailSolicitante",
                table: "Tickets",
                newName: "Responsavel");
        }
    }
}
