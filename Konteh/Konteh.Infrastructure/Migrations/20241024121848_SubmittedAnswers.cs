using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Konteh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubmittedAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_ExamQuestions_ExamQuestionId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_ExamQuestionId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "ExamQuestionId",
                table: "Answers");

            migrationBuilder.CreateTable(
                name: "SubmittedAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerId = table.Column<long>(type: "bigint", nullable: false),
                    ExamQuestionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmittedAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmittedAnswers_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubmittedAnswers_ExamQuestions_ExamQuestionId",
                        column: x => x.ExamQuestionId,
                        principalTable: "ExamQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedAnswers_AnswerId",
                table: "SubmittedAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedAnswers_ExamQuestionId",
                table: "SubmittedAnswers",
                column: "ExamQuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubmittedAnswers");

            migrationBuilder.AddColumn<long>(
                name: "ExamQuestionId",
                table: "Answers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ExamQuestionId",
                table: "Answers",
                column: "ExamQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_ExamQuestions_ExamQuestionId",
                table: "Answers",
                column: "ExamQuestionId",
                principalTable: "ExamQuestions",
                principalColumn: "Id");
        }
    }
}
