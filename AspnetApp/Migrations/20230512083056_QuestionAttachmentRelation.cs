using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetApp.Migrations
{
    /// <inheritdoc />
    public partial class QuestionAttachmentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Attachments_AttachmentId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_AttachmentId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Attachments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_QuestionId",
                table: "Attachments",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Questions_QuestionId",
                table: "Attachments",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Questions_QuestionId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_QuestionId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Attachments");

            migrationBuilder.AddColumn<int>(
                name: "AttachmentId",
                table: "Questions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AttachmentId",
                table: "Questions",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Attachments_AttachmentId",
                table: "Questions",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id");
        }
    }
}
