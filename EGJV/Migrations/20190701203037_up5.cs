using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EGJV.Migrations
{
    public partial class up5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderAssets");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderCards");

            migrationBuilder.DropTable(
                name: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderCards",
                columns: table => new
                {
                    OrderCardId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<long>(nullable: true),
                    TicketId = table.Column<long>(nullable: true),
                    UserTUserId = table.Column<long>(nullable: true),
                    orderCardToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCards", x => x.OrderCardId);
                    table.ForeignKey(
                        name: "FK_OrderCards_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderCards_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderCards_TUsers_UserTUserId",
                        column: x => x.UserTUserId,
                        principalTable: "TUsers",
                        principalColumn: "TUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderCardId = table.Column<long>(nullable: true),
                    StatusID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_OrderCards_OrderCardId",
                        column: x => x.OrderCardId,
                        principalTable: "OrderCards",
                        principalColumn: "OrderCardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderAssets",
                columns: table => new
                {
                    OrderProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssetId = table.Column<long>(nullable: true),
                    OrderId = table.Column<long>(nullable: true),
                    Quantity = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAssets", x => x.OrderProductId);
                    table.ForeignKey(
                        name: "FK_OrderAssets_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "AssetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderAssets_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderAssets_AssetId",
                table: "OrderAssets",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAssets_OrderId",
                table: "OrderAssets",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCards_CompanyId",
                table: "OrderCards",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCards_TicketId",
                table: "OrderCards",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCards_UserTUserId",
                table: "OrderCards",
                column: "UserTUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderCardId",
                table: "Orders",
                column: "OrderCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusID",
                table: "Orders",
                column: "StatusID");
        }
    }
}
