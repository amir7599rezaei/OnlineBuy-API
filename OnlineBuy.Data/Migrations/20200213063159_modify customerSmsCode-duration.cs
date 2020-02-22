using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBuy.Data.Migrations
{
    public partial class modifycustomerSmsCodeduration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DurationCode",
                table: "CustomerSmsCodes",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "DurationCode",
                table: "CustomerSmsCodes",
                type: "time",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
