using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBuy.Data.Migrations
{
    public partial class editproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Customers_CustomerId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Customers_CustomerId",
                table: "Products",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Customers_CustomerId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Customers_CustomerId",
                table: "Products",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
