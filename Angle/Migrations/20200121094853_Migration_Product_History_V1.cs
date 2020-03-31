using Microsoft.EntityFrameworkCore.Migrations;

namespace Angle.Migrations
{
    public partial class Migration_Product_History_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "History");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "ProductHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BootStrapBadge",
                table: "History",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_UserID",
                table: "ProductHistory",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistory_AspNetUsers_UserID",
                table: "ProductHistory",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistory_AspNetUsers_UserID",
                table: "ProductHistory");

            migrationBuilder.DropIndex(
                name: "IX_ProductHistory_UserID",
                table: "ProductHistory");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "ProductHistory");

            migrationBuilder.DropColumn(
                name: "BootStrapBadge",
                table: "History");

            migrationBuilder.AddColumn<int>(
                name: "TypeID",
                table: "History",
                nullable: false,
                defaultValue: 0);
        }
    }
}
