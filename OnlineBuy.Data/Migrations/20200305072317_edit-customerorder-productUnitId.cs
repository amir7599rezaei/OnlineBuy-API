using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBuy.Data.Migrations
{
    public partial class editcustomerorderproductUnitId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_ProductUnits_ProductUnitId1",
                table: "CustomerOrders");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrders_ProductUnitId1",
                table: "CustomerOrders");

            migrationBuilder.DropColumn(
                name: "ProductUnitId1",
                table: "CustomerOrders");

            migrationBuilder.AlterColumn<int>(
                name: "ProductUnitId",
                table: "CustomerOrders",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_ProductUnitId",
                table: "CustomerOrders",
                column: "ProductUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_ProductUnits_ProductUnitId",
                table: "CustomerOrders",
                column: "ProductUnitId",
                principalTable: "ProductUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_ProductUnits_ProductUnitId",
                table: "CustomerOrders");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrders_ProductUnitId",
                table: "CustomerOrders");

            migrationBuilder.AlterColumn<string>(
                name: "ProductUnitId",
                table: "CustomerOrders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProductUnitId1",
                table: "CustomerOrders",
                type: "int",
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
    }
}
