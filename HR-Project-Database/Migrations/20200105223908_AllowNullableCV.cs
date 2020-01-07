using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_Project_Database.Migrations
{
    public partial class AllowNullableCV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVId",
                table: "Application");

            migrationBuilder.AddColumn<Guid>(
                name: "CVId",
                table: "Application",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVId",
                table: "Application");

            migrationBuilder.AddColumn<int>(
                name: "CVId",
                table: "Application",
                nullable: false);
        }
    }
}
