using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace question.model.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "questionDb");

            migrationBuilder.CreateTable(
                name: "tbQuestion",
                schema: "questionDb",
                columns: table => new
                {
                    questionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    questionName = table.Column<string>(nullable: true),
                    imageUrl = table.Column<string>(nullable: true),
                    thumbUrl = table.Column<string>(nullable: true),
                    publishedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("questionID", x => x.questionID);
                });

            migrationBuilder.CreateTable(
                name: "tbChoise",
                schema: "questionDb",
                columns: table => new
                {
                    choiceName = table.Column<string>(nullable: true),
                    votes = table.Column<int>(nullable: false),
                    choiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("choiceId", x => x.choiceId);
                    table.ForeignKey(
                        name: "FK_tbChoise_tbQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "questionDb",
                        principalTable: "tbQuestion",
                        principalColumn: "questionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbChoise_QuestionId",
                schema: "questionDb",
                table: "tbChoise",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbChoise",
                schema: "questionDb");

            migrationBuilder.DropTable(
                name: "tbQuestion",
                schema: "questionDb");
        }
    }
}
