using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionBanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionBanks_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherClasses_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Statement = table.Column<string>(type: "text", nullable: false),
                    QuestionBankId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionBanks_QuestionBankId",
                        column: x => x.QuestionBankId,
                        principalTable: "QuestionBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    TeacherClassId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_TeacherClasses_TeacherClassId",
                        column: x => x.TeacherClassId,
                        principalTable: "TeacherClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestWrittens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    RegexQuestionIdentifier = table.Column<string>(type: "text", nullable: false),
                    TeacherClassId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestWrittens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestWrittens_TeacherClasses_TeacherClassId",
                        column: x => x.TeacherClassId,
                        principalTable: "TeacherClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionCriterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Criteria = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCriterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionCriterias_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Corrections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestWrittenId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corrections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Corrections_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Corrections_TestWrittens_TestWrittenId",
                        column: x => x.TestWrittenId,
                        principalTable: "TestWrittens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTestWrittens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    Lines = table.Column<int>(type: "integer", nullable: false),
                    TestWrittenId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTestWrittens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionTestWrittens_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionTestWrittens_TestWrittens_TestWrittenId",
                        column: x => x.TestWrittenId,
                        principalTable: "TestWrittens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentAnswer = table.Column<string>(type: "text", nullable: false),
                    Score = table.Column<double>(type: "double precision", nullable: false),
                    CorrectionId = table.Column<int>(type: "integer", nullable: false),
                    QuestionTestWrittenId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Corrections_CorrectionId",
                        column: x => x.CorrectionId,
                        principalTable: "Corrections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_QuestionTestWrittens_QuestionTestWrittenId",
                        column: x => x.QuestionTestWrittenId,
                        principalTable: "QuestionTestWrittens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    AnswerAiId = table.Column<int>(type: "integer", nullable: true),
                    AnswerTeacherId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Answers_AnswerAiId",
                        column: x => x.AnswerAiId,
                        principalTable: "Answers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Answers_AnswerTeacherId",
                        column: x => x.AnswerTeacherId,
                        principalTable: "Answers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CorrectionId",
                table: "Answers",
                column: "CorrectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_Id",
                table: "Answers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionTestWrittenId",
                table: "Answers",
                column: "QuestionTestWrittenId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AnswerAiId",
                table: "Comments",
                column: "AnswerAiId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AnswerTeacherId",
                table: "Comments",
                column: "AnswerTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Id",
                table: "Comments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Corrections_Id",
                table: "Corrections",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Corrections_StudentId",
                table: "Corrections",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Corrections_TestWrittenId",
                table: "Corrections",
                column: "TestWrittenId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBanks_Id",
                table: "QuestionBanks",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBanks_TeacherId",
                table: "QuestionBanks",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCriterias_Id",
                table: "QuestionCriterias",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCriterias_QuestionId",
                table: "QuestionCriterias",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Id",
                table: "Questions",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionBankId",
                table: "Questions",
                column: "QuestionBankId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTestWrittens_Id",
                table: "QuestionTestWrittens",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTestWrittens_QuestionId",
                table: "QuestionTestWrittens",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTestWrittens_TestWrittenId",
                table: "QuestionTestWrittens",
                column: "TestWrittenId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Id",
                table: "Students",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_TeacherClassId",
                table: "Students",
                column: "TeacherClassId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherClasses_Id",
                table: "TeacherClasses",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherClasses_TeacherId",
                table: "TeacherClasses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_Id",
                table: "Teachers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestWrittens_Id",
                table: "TestWrittens",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestWrittens_TeacherClassId",
                table: "TestWrittens",
                column: "TeacherClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "QuestionCriterias");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Corrections");

            migrationBuilder.DropTable(
                name: "QuestionTestWrittens");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "TestWrittens");

            migrationBuilder.DropTable(
                name: "QuestionBanks");

            migrationBuilder.DropTable(
                name: "TeacherClasses");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
