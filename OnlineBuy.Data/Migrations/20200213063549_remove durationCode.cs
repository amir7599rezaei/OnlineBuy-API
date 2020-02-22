using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBuy.Data.Migrations
{
    public partial class removedurationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationCode",
                table: "CustomerSmsCodes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DurationCode",
                table: "CustomerSmsCodes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
