using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angle.Migrations
{
    public partial class Create_migration_QRIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Index_QR",
                table: "Index_QR");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Index_QR",
                nullable: false,
                defaultValue: 0L)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Index_QR",
                table: "Index_QR",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Index_QR_ActionID",
                table: "Index_QR",
                column: "ActionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Index_QR",
                table: "Index_QR");

            migrationBuilder.DropIndex(
                name: "IX_Index_QR_ActionID",
                table: "Index_QR");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Index_QR");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Index_QR",
                table: "Index_QR",
                columns: new[] { "ActionID", "ControllerID", "ProductID" });
        }
    }
}
