using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBuy.Data.Migrations
{
    public partial class modify_log : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Customers_CustomerId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "CusromerId",
                table: "Logs");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Logs",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Customers_CustomerId",
                table: "Logs",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Customers_CustomerId",
                table: "Logs");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Logs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "CusromerId",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Customers_CustomerId",
                table: "Logs",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
