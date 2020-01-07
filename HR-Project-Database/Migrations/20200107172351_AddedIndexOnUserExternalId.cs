using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_Project_Database.Migrations
{
    public partial class AddedIndexOnUserExternalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_ExternalId",
                table: "User",
                column: "ExternalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_ExternalId",
                table: "User");
        }
    }
}
