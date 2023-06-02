using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetApp.Migrations
{
    /// <inheritdoc />
    public partial class super : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CorrectAnswer",
                table: "QuizResults",
                newName: "CorrectAnswers");

            migrationBuilder.AddColumn<bool>(
                name: "CorrectAnswer",
                table: "SolvedQuestions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "SolvedQuestions");

            migrationBuilder.RenameColumn(
                name: "CorrectAnswers",
                table: "QuizResults",
                newName: "CorrectAnswer");
        }
    }
}
