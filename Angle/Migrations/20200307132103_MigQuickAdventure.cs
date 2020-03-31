using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angle.Migrations
{
    public partial class MigQuickAdventure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "DeviceTypeID",
                table: "Product",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "QuickAdventure",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    ProductID = table.Column<long>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuickAdventure", x => new { x.ProductID, x.UserID });
                    table.ForeignKey(
                        name: "FK_QuickAdventure_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuickAdventure_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeviceTypeID",
                table: "Product",
                column: "DeviceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_QuickAdventure_UserID",
                table: "QuickAdventure",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_PType_DeviceTypeID",
                table: "Product",
                column: "DeviceTypeID",
                principalTable: "PType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_PType_DeviceTypeID",
                table: "Product");

            migrationBuilder.DropTable(
                name: "QuickAdventure");

            migrationBuilder.DropIndex(
                name: "IX_Product_DeviceTypeID",
                table: "Product");

            migrationBuilder.AlterColumn<long>(
                name: "DeviceTypeID",
                table: "Product",
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}
