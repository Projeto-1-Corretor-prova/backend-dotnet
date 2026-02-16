using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class deletions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Answers_AnswerAiId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Answers_AnswerTeacherId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Answers_AnswerAiId",
                table: "Comments",
                column: "AnswerAiId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Answers_AnswerTeacherId",
                table: "Comments",
                column: "AnswerTeacherId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Answers_AnswerAiId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Answers_AnswerTeacherId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Answers_AnswerAiId",
                table: "Comments",
                column: "AnswerAiId",
                principalTable: "Answers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Answers_AnswerTeacherId",
                table: "Comments",
                column: "AnswerTeacherId",
                principalTable: "Answers",
                principalColumn: "Id");
        }
    }
}
