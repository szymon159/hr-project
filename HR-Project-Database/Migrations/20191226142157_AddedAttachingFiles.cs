using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_Project_Database.Migrations
{
    public partial class AddedAttachingFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_CV",
                table: "Application");

            migrationBuilder.DropTable(
                name: "CV");

            migrationBuilder.DropIndex(
                name: "IX_Application_CVId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "AttachmentPath",
                table: "Attachment");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Attachment",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Attachment");

            migrationBuilder.AddColumn<string>(
                name: "AttachmentPath",
                table: "Attachment",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CV",
                columns: table => new
                {
                    Id_CV = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CVPath = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV", x => x.Id_CV);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Application_CVId",
                table: "Application",
                column: "CVId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_CV",
                table: "Application",
                column: "CVId",
                principalTable: "CV",
                principalColumn: "Id_CV",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
