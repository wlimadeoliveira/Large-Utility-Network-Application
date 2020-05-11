using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angle.Migrations
{
    public partial class Create_Table_QR_Index_Controller_Action : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "DeviceTypeID",
                table: "Product",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Action_QR",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action_QR", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Controller_QR",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controller_QR", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Index_QR",
                columns: table => new
                {
                    ActionID = table.Column<long>(nullable: false),
                    ControllerID = table.Column<long>(nullable: false),
                    ProductID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Parameter = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Index_QR", x => new { x.ActionID, x.ControllerID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_Index_QR_Action_QR_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Action_QR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Index_QR_Controller_QR_ControllerID",
                        column: x => x.ControllerID,
                        principalTable: "Controller_QR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Index_QR_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "UserID",
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
                name: "IX_Index_QR_ControllerID",
                table: "Index_QR",
                column: "ControllerID");

            migrationBuilder.CreateIndex(
                name: "IX_Index_QR_ProductID",
                table: "Index_QR",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Index_QR_UserID",
                table: "Index_QR",
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
                name: "Index_QR");

            migrationBuilder.DropTable(
                name: "Action_QR");

            migrationBuilder.DropTable(
                name: "Controller_QR");

            migrationBuilder.DropIndex(
                name: "IX_Product_DeviceTypeID",
                table: "Product");

            migrationBuilder.AlterColumn<long>(
                name: "DeviceTypeID",
                table: "Product",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}
