using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LT.TechLabHackathon.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChallengeLevels",
                columns: table => new
                {
                    LevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeLevels", x => x.LevelId);
                });

            migrationBuilder.CreateTable(
                name: "ParameterTypes",
                columns: table => new
                {
                    ParameterTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterTypes", x => x.ParameterTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammingLanguages",
                columns: table => new
                {
                    ProgrammingLanguageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguages", x => x.ProgrammingLanguageId);
                });

            migrationBuilder.CreateTable(
                name: "Challenges",
                columns: table => new
                {
                    ChallengeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.ChallengeId);
                    table.ForeignKey(
                        name: "FK_Challenges_ChallengeLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "ChallengeLevels",
                        principalColumn: "LevelId");
                });

            migrationBuilder.CreateTable(
                name: "ProgrammingLanguageReservedWords",
                columns: table => new
                {
                    ProgrammingLanguageReservedWordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgrammingLanguageId = table.Column<int>(type: "int", nullable: false),
                    ReservedWord = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguageReservedWords", x => x.ProgrammingLanguageReservedWordId);
                    table.ForeignKey(
                        name: "FK_ProgrammingLanguageReservedWords_ProgrammingLanguages_ProgrammingLanguageId",
                        column: x => x.ProgrammingLanguageId,
                        principalTable: "ProgrammingLanguages",
                        principalColumn: "ProgrammingLanguageId");
                });

            migrationBuilder.CreateTable(
                name: "ChallengeConstraints",
                columns: table => new
                {
                    ConstraintId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeConstraints", x => x.ConstraintId);
                    table.ForeignKey(
                        name: "FK_ChallengeConstraints_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "ChallengeId");
                });

            migrationBuilder.CreateTable(
                name: "ChallengeExamples",
                columns: table => new
                {
                    ExampleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    ValidationId = table.Column<int>(type: "int", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeExamples", x => x.ExampleId);
                    table.ForeignKey(
                        name: "FK_ChallengeExamples_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "ChallengeId");
                });

            migrationBuilder.CreateTable(
                name: "ChallengeLanguageSignatures",
                columns: table => new
                {
                    LanguageSignatureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    ProgrammingLanguageId = table.Column<int>(type: "int", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeLanguageSignatures", x => x.LanguageSignatureId);
                    table.ForeignKey(
                        name: "FK_ChallengeLanguageSignatures_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "ChallengeId");
                    table.ForeignKey(
                        name: "FK_ChallengeLanguageSignatures_ProgrammingLanguages_ProgrammingLanguageId",
                        column: x => x.ProgrammingLanguageId,
                        principalTable: "ProgrammingLanguages",
                        principalColumn: "ProgrammingLanguageId");
                });

            migrationBuilder.CreateTable(
                name: "ChallengeValidations",
                columns: table => new
                {
                    ValidationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    ParameterTypeId = table.Column<int>(type: "int", nullable: false),
                    OutputValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeValidations", x => x.ValidationId);
                    table.ForeignKey(
                        name: "FK_ChallengeValidations_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "ChallengeId");
                    table.ForeignKey(
                        name: "FK_ChallengeValidations_ParameterTypes_ParameterTypeId",
                        column: x => x.ParameterTypeId,
                        principalTable: "ParameterTypes",
                        principalColumn: "ParameterTypeId");
                });

            migrationBuilder.CreateTable(
                name: "ChallengeInputParameters",
                columns: table => new
                {
                    InputParameterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidationId = table.Column<int>(type: "int", nullable: false),
                    ParameterTypeId = table.Column<int>(type: "int", nullable: false),
                    InputValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeInputParameters", x => x.InputParameterId);
                    table.ForeignKey(
                        name: "FK_ChallengeInputParameters_ChallengeValidations_ValidationId",
                        column: x => x.ValidationId,
                        principalTable: "ChallengeValidations",
                        principalColumn: "ValidationId");
                    table.ForeignKey(
                        name: "FK_ChallengeInputParameters_ParameterTypes_ParameterTypeId",
                        column: x => x.ParameterTypeId,
                        principalTable: "ParameterTypes",
                        principalColumn: "ParameterTypeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeConstraints_ChallengeId",
                table: "ChallengeConstraints",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeExamples_ChallengeId",
                table: "ChallengeExamples",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeInputParameters_ParameterTypeId",
                table: "ChallengeInputParameters",
                column: "ParameterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeInputParameters_ValidationId",
                table: "ChallengeInputParameters",
                column: "ValidationId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeLanguageSignatures_ChallengeId",
                table: "ChallengeLanguageSignatures",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeLanguageSignatures_ProgrammingLanguageId",
                table: "ChallengeLanguageSignatures",
                column: "ProgrammingLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_LevelId",
                table: "Challenges",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeValidations_ChallengeId",
                table: "ChallengeValidations",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeValidations_ParameterTypeId",
                table: "ChallengeValidations",
                column: "ParameterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammingLanguageReservedWords_ProgrammingLanguageId",
                table: "ProgrammingLanguageReservedWords",
                column: "ProgrammingLanguageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeConstraints");

            migrationBuilder.DropTable(
                name: "ChallengeExamples");

            migrationBuilder.DropTable(
                name: "ChallengeInputParameters");

            migrationBuilder.DropTable(
                name: "ChallengeLanguageSignatures");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguageReservedWords");

            migrationBuilder.DropTable(
                name: "ChallengeValidations");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguages");

            migrationBuilder.DropTable(
                name: "Challenges");

            migrationBuilder.DropTable(
                name: "ParameterTypes");

            migrationBuilder.DropTable(
                name: "ChallengeLevels");
        }
    }
}
