using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBuy.Data.Migrations
{
    public partial class addcustomerorderproductUnitId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductUnitId",
                table: "CustomerOrders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductUnitId1",
                table: "CustomerOrders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_ProductUnitId1",
                table: "CustomerOrders",
                column: "ProductUnitId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_ProductUnits_ProductUnitId1",
                table: "CustomerOrders",
                column: "ProductUnitId1",
                principalTable: "ProductUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_ProductUnits_ProductUnitId1",
                table: "CustomerOrders");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrders_ProductUnitId1",
                table: "CustomerOrders");

            migrationBuilder.DropColumn(
                name: "ProductUnitId",
                table: "CustomerOrders");

            migrationBuilder.DropColumn(
                name: "ProductUnitId1",
                table: "CustomerOrders");
        }
    }
}
