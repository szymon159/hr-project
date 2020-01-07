using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR_Project_Database.Migrations
{
    public partial class AddedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationMessage",
                columns: new[] { "Id_ApplicationMessage", "MessageContent" },
                values: new object[] { 1, "Test message content" });

            migrationBuilder.InsertData(
                table: "AttachmentGroup",
                column: "Id_AttachmentGroup",
                value: 1);

            migrationBuilder.InsertData(
                table: "JobOffer",
                columns: new[] { "Id_JobOffer", "AttachmentGroupId", "Description", "JobTitle", "Status" },
                values: new object[,]
                {
                    { 1, null, "Backend Developer with id = 1 and experience in creating bugs", "Backend Developer", 0 },
                    { 2, null, "Frontend Developer with id = 2 and experience in creating requirements for nice-looking things which are unable to implement", "Frontend Developer", 0 },
                    { 3, null, "Manager with id = 3 and experience in managing things (doesn't really matter what kind of things)", "Manager", 0 },
                    { 4, null, "Teacher with id = 4 who is ready to earn less than he should for his skills", "Teacher", 0 },
                    { 5, null, "Finally some good funny person", "Cook", 0 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id_User", "Email", "ExternalId", "FirstName", "LastName", "Role" },
                values: new object[,]
                {
                    { 1, "a", "1", "Adam", "Małysz", 1 },
                    { 2, "s.stasiak@student.mini.pw.edu.pl", "2", "Dawid", "Kubacki", 2 }
                });

            migrationBuilder.InsertData(
                table: "Application",
                columns: new[] { "Id_Application", "ApplicationMessageId", "AttachmentGroupId", "CVId", "JobOfferId", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, new Guid("3b8cbf16-a004-45ef-bfb4-7364a3c67945"), 1, 2, 1 },
                    { 2, null, null, new Guid("b35d7728-fb45-47cd-9794-bd54ea417bb4"), 2, 3, 1 },
                    { 3, null, null, new Guid("3b8cbf16-a004-45ef-bfb4-7364a3c67945"), 3, 1, 1 },
                    { 4, null, 1, new Guid("b35d7728-fb45-47cd-9794-bd54ea417bb4"), 4, 1, 1 },
                    { 5, null, 1, new Guid("cfabe3bc-66b7-49e5-9bf4-be0e19520140"), 5, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Attachment",
                columns: new[] { "Id_Attachment", "AttachmentGroupId", "Extension" },
                values: new object[,]
                {
                    { 1, 1, ".pdf" },
                    { 2, 1, ".pdf" }
                });

            migrationBuilder.InsertData(
                table: "Responsibility",
                columns: new[] { "Id_Responsibility", "JobOfferId", "UserId" },
                values: new object[,]
                {
                    { 1, 2, 2 },
                    { 2, 5, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id_Application",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id_Application",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id_Application",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id_Application",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id_Application",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Attachment",
                keyColumn: "Id_Attachment",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Attachment",
                keyColumn: "Id_Attachment",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Responsibility",
                keyColumn: "Id_Responsibility",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Responsibility",
                keyColumn: "Id_Responsibility",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ApplicationMessage",
                keyColumn: "Id_ApplicationMessage",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AttachmentGroup",
                keyColumn: "Id_AttachmentGroup",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobOffer",
                keyColumn: "Id_JobOffer",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobOffer",
                keyColumn: "Id_JobOffer",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JobOffer",
                keyColumn: "Id_JobOffer",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "JobOffer",
                keyColumn: "Id_JobOffer",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "JobOffer",
                keyColumn: "Id_JobOffer",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id_User",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id_User",
                keyValue: 2);
        }
    }
}
