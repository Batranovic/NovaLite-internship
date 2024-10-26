using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Konteh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamQuestionAnswer");

            migrationBuilder.CreateTable(
                name: "AnswerExamQuestion",
                columns: table => new
                {
                    ExamQuestionId = table.Column<long>(type: "bigint", nullable: false),
                    SubmittedAnswersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerExamQuestion", x => new { x.ExamQuestionId, x.SubmittedAnswersId });
                    table.ForeignKey(
                        name: "FK_AnswerExamQuestion_Answers_SubmittedAnswersId",
                        column: x => x.SubmittedAnswersId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerExamQuestion_ExamQuestions_ExamQuestionId",
                        column: x => x.ExamQuestionId,
                        principalTable: "ExamQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerExamQuestion_SubmittedAnswersId",
                table: "AnswerExamQuestion",
                column: "SubmittedAnswersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerExamQuestion");

            migrationBuilder.CreateTable(
                name: "ExamQuestionAnswer",
                columns: table => new
                {
                    AnswerId = table.Column<long>(type: "bigint", nullable: false),
                    ExamQuestionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQuestionAnswer", x => new { x.AnswerId, x.ExamQuestionId });
                    table.ForeignKey(
                        name: "FK_ExamQuestionAnswer_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamQuestionAnswer_ExamQuestions_ExamQuestionId",
                        column: x => x.ExamQuestionId,
                        principalTable: "ExamQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestionAnswer_ExamQuestionId",
                table: "ExamQuestionAnswer",
                column: "ExamQuestionId");
        }
    }
}
