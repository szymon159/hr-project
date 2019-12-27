using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_Project_Database.Migrations
{
    public partial class AddedLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "User",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "User",
                nullable: false,
                defaultValue: 0);
        }
    }
}
