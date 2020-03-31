using Microsoft.EntityFrameworkCore.Migrations;

namespace Angle.Migrations
{
    public partial class Ivy1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SupplierID",
                table: "Article",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "ManufacturerID",
                table: "Article",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "LocationID",
                table: "Article",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SupplierID",
                table: "Article",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ManufacturerID",
                table: "Article",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LocationID",
                table: "Article",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
