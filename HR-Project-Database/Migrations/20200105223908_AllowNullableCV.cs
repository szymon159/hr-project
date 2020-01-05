using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_Project_Database.Migrations
{
    public partial class AllowNullableCV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CVId",
                table: "Application",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "Application",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
