using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_Project_Database.Migrations
{
    public partial class InitialMigrationNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationMessage",
                columns: table => new
                {
                    Id_ApplicationMessage = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MessageContent = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationMessage", x => x.Id_ApplicationMessage);
                });

            migrationBuilder.CreateTable(
                name: "AttachmentGroup",
                columns: table => new
                {
                    Id_AttachmentGroup = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentGroup", x => x.Id_AttachmentGroup);
                });

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

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id_User = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Role = table.Column<int>(nullable: false),
                    UserProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id_User);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id_Attachment = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentPath = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    AttachmentGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id_Attachment);
                    table.ForeignKey(
                        name: "FK_Attachment_AttachmentGroup",
                        column: x => x.AttachmentGroupId,
                        principalTable: "AttachmentGroup",
                        principalColumn: "Id_AttachmentGroup",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobOffer",
                columns: table => new
                {
                    Id_JobOffer = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobTitle = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    AttachmentGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOffer", x => x.Id_JobOffer);
                    table.ForeignKey(
                        name: "FK_JobOffer_AttachmentGroup",
                        column: x => x.AttachmentGroupId,
                        principalTable: "AttachmentGroup",
                        principalColumn: "Id_AttachmentGroup",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id_Application = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobOfferId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CVId = table.Column<int>(nullable: false),
                    AttachmentGroupId = table.Column<int>(nullable: true),
                    ApplicationMessageId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id_Application);
                    table.ForeignKey(
                        name: "FK_Application_ApplicationMessage",
                        column: x => x.ApplicationMessageId,
                        principalTable: "ApplicationMessage",
                        principalColumn: "Id_ApplicationMessage",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Application_AttachmentGroup",
                        column: x => x.AttachmentGroupId,
                        principalTable: "AttachmentGroup",
                        principalColumn: "Id_AttachmentGroup",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Application_CV",
                        column: x => x.CVId,
                        principalTable: "CV",
                        principalColumn: "Id_CV",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Application_JobOffer",
                        column: x => x.JobOfferId,
                        principalTable: "JobOffer",
                        principalColumn: "Id_JobOffer",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Application_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Responsibility",
                columns: table => new
                {
                    Id_Responsibility = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    JobOfferId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsibility", x => x.Id_Responsibility);
                    table.ForeignKey(
                        name: "FK_Responsibility_JobOffer",
                        column: x => x.JobOfferId,
                        principalTable: "JobOffer",
                        principalColumn: "Id_JobOffer",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Responsibility_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Application_ApplicationMessageId",
                table: "Application",
                column: "ApplicationMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_AttachmentGroupId",
                table: "Application",
                column: "AttachmentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_CVId",
                table: "Application",
                column: "CVId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_JobOfferId",
                table: "Application",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_UserId",
                table: "Application",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_AttachmentGroupId",
                table: "Attachment",
                column: "AttachmentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOffer_AttachmentGroupId",
                table: "JobOffer",
                column: "AttachmentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Responsibility_JobOfferId",
                table: "Responsibility",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Responsibility_UserId",
                table: "Responsibility",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "Responsibility");

            migrationBuilder.DropTable(
                name: "ApplicationMessage");

            migrationBuilder.DropTable(
                name: "CV");

            migrationBuilder.DropTable(
                name: "JobOffer");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "AttachmentGroup");
        }
    }
}
