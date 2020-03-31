using Microsoft.EntityFrameworkCore.Migrations;

namespace Angle.Migrations
{
    public partial class AlternateKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProductHistory_Date",
                table: "ProductHistory",
                column: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProductHistory_Date",
                table: "ProductHistory");
        }
    }
}
