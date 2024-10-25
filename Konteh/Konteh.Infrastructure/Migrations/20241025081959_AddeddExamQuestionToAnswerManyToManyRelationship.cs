using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Konteh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddeddExamQuestionToAnswerManyToManyRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubmittedAnswers");

            migrationBuilder.CreateTable(
                name: "AnswerExamQuestion",
                columns: table => new
                {
                    ExamQuestionId = table.Column<long>(type: "bigint", nullable: false),
                    SubmmitedAnswersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerExamQuestion", x => new { x.ExamQuestionId, x.SubmmitedAnswersId });
                    table.ForeignKey(
                        name: "FK_AnswerExamQuestion_Answers_SubmmitedAnswersId",
                        column: x => x.SubmmitedAnswersId,
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
                name: "IX_AnswerExamQuestion_SubmmitedAnswersId",
                table: "AnswerExamQuestion",
                column: "SubmmitedAnswersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerExamQuestion");

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
    }
}
