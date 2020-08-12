using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angle.Migrations
{
    public partial class ProductSoftwareTypeAndOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SoftwareTypeID",
                table: "PType",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SoftwareOptions",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DataType = table.Column<string>(nullable: true),
                    DataTypeValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareOptions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareTypes",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductSoftwareOptions",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true),
                    ProductID = table.Column<long>(nullable: false),
                    SoftwareTypeID = table.Column<long>(nullable: false),
                    SoftwareOptionID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSoftwareOptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductSoftwareOptions_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSoftwareOptions_SoftwareOptions_SoftwareOptionID",
                        column: x => x.SoftwareOptionID,
                        principalTable: "SoftwareOptions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSoftwareOptions_SoftwareTypes_SoftwareTypeID",
                        column: x => x.SoftwareTypeID,
                        principalTable: "SoftwareTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareTypeOptions",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SoftwareTypeID = table.Column<long>(nullable: false),
                    SoftwareOptionID = table.Column<long>(nullable: false),
                    ProductID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareTypeOptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SoftwareTypeOptions_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoftwareTypeOptions_SoftwareOptions_SoftwareOptionID",
                        column: x => x.SoftwareOptionID,
                        principalTable: "SoftwareOptions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoftwareTypeOptions_SoftwareTypes_SoftwareTypeID",
                        column: x => x.SoftwareTypeID,
                        principalTable: "SoftwareTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PType_SoftwareTypeID",
                table: "PType",
                column: "SoftwareTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSoftwareOptions_ProductID",
                table: "ProductSoftwareOptions",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSoftwareOptions_SoftwareOptionID",
                table: "ProductSoftwareOptions",
                column: "SoftwareOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSoftwareOptions_SoftwareTypeID",
                table: "ProductSoftwareOptions",
                column: "SoftwareTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareTypeOptions_ProductID",
                table: "SoftwareTypeOptions",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareTypeOptions_SoftwareOptionID",
                table: "SoftwareTypeOptions",
                column: "SoftwareOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareTypeOptions_SoftwareTypeID",
                table: "SoftwareTypeOptions",
                column: "SoftwareTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_PType_SoftwareTypes_SoftwareTypeID",
                table: "PType",
                column: "SoftwareTypeID",
                principalTable: "SoftwareTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PType_SoftwareTypes_SoftwareTypeID",
                table: "PType");

            migrationBuilder.DropTable(
                name: "ProductSoftwareOptions");

            migrationBuilder.DropTable(
                name: "SoftwareTypeOptions");

            migrationBuilder.DropTable(
                name: "SoftwareOptions");

            migrationBuilder.DropTable(
                name: "SoftwareTypes");

            migrationBuilder.DropIndex(
                name: "IX_PType_SoftwareTypeID",
                table: "PType");

            migrationBuilder.DropColumn(
                name: "SoftwareTypeID",
                table: "PType");
        }
    }
}
