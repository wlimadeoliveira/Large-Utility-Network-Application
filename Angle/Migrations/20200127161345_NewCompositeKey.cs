using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angle.Migrations
{
    public partial class NewCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductHistory",
                columns: table => new
                {
                    ProductID = table.Column<long>(nullable: false),
                    HistoryID = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHistory", x => new { x.ProductID, x.HistoryID, x.UserID, x.Date });
                    table.ForeignKey(
                        name: "FK_ProductHistory_History_HistoryID",
                        column: x => x.HistoryID,
                        principalTable: "History",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductHistory_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductHistory_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_HistoryID",
                table: "ProductHistory",
                column: "HistoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_UserID",
                table: "ProductHistory",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
