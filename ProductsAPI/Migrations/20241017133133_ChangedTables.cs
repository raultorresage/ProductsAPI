using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Bills_BillId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BillId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "BillProductIds",
                table: "Bills",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillProductIds",
                table: "Bills");

            migrationBuilder.AddColumn<string>(
                name: "BillId",
                table: "Products",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BillId",
                table: "Products",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Bills_BillId",
                table: "Products",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");
        }
    }
}
