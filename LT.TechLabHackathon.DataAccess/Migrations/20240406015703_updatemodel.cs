using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LT.TechLabHackathon.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChallengeInputParameters_ParameterTypes_ParameterTypeId",
                table: "ChallengeInputParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_ChallengeValidations_ParameterTypes_ParameterTypeId",
                table: "ChallengeValidations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChallenge_Challenges_ChallengeId",
                table: "UserChallenge");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChallenge_Users_UserId",
                table: "UserChallenge");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChallengeHistory_Challenges_ChallengeId",
                table: "UserChallengeHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChallengeHistory_Users_UserId",
                table: "UserChallengeHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Status_StatusId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserScore_Users_UserId",
                table: "UserScore");

            migrationBuilder.DropTable(
                name: "ParameterTypes");

            migrationBuilder.DropIndex(
                name: "IX_ChallengeValidations_ParameterTypeId",
                table: "ChallengeValidations");

            migrationBuilder.DropIndex(
                name: "IX_ChallengeInputParameters_ParameterTypeId",
                table: "ChallengeInputParameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserScore",
                table: "UserScore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserChallengeHistory",
                table: "UserChallengeHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserChallenge",
                table: "UserChallenge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "ParameterTypeId",
                table: "ChallengeValidations");

            migrationBuilder.RenameTable(
                name: "UserScore",
                newName: "UserScores");

            migrationBuilder.RenameTable(
                name: "UserChallengeHistory",
                newName: "UserChallengeHistories");

            migrationBuilder.RenameTable(
                name: "UserChallenge",
                newName: "UserChallenges");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "Statuses");

            migrationBuilder.RenameColumn(
                name: "ParameterTypeId",
                table: "ChallengeInputParameters",
                newName: "Sequence");

            migrationBuilder.RenameIndex(
                name: "IX_UserScore_UserId",
                table: "UserScores",
                newName: "IX_UserScores_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserChallengeHistory_UserId",
                table: "UserChallengeHistories",
                newName: "IX_UserChallengeHistories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserChallengeHistory_ChallengeId",
                table: "UserChallengeHistories",
                newName: "IX_UserChallengeHistories_ChallengeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserChallenge_UserId",
                table: "UserChallenges",
                newName: "IX_UserChallenges_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserChallenge_ChallengeId",
                table: "UserChallenges",
                newName: "IX_UserChallenges_ChallengeId");

            migrationBuilder.AddColumn<int>(
                name: "InputParameters",
                table: "Challenges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MethodName",
                table: "Challenges",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ResultDataTypeId",
                table: "Challenges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserScores",
                table: "UserScores",
                column: "UserScoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserChallengeHistories",
                table: "UserChallengeHistories",
                column: "UserChallengeHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserChallenges",
                table: "UserChallenges",
                column: "UserChallengeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "StatusId");

            migrationBuilder.CreateTable(
                name: "GeneralDataTypes",
                columns: table => new
                {
                    DataTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralDataTypes", x => x.DataTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeInputSetupParameters",
                columns: table => new
                {
                    ChallengeInputSetupParameterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ParameterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeInputSetupParameters", x => x.ChallengeInputSetupParameterId);
                    table.ForeignKey(
                        name: "FK_ChallengeInputSetupParameters_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "ChallengeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChallengeInputSetupParameters_GeneralDataTypes_DataTypeId",
                        column: x => x.DataTypeId,
                        principalTable: "GeneralDataTypes",
                        principalColumn: "DataTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammingLanguageDataTypes",
                columns: table => new
                {
                    ProgrammingLanguageDataTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataTypeId = table.Column<int>(type: "int", nullable: false),
                    ProgrammingLanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguageDataTypes", x => x.ProgrammingLanguageDataTypeId);
                    table.ForeignKey(
                        name: "FK_ProgrammingLanguageDataTypes_GeneralDataTypes_DataTypeId",
                        column: x => x.DataTypeId,
                        principalTable: "GeneralDataTypes",
                        principalColumn: "DataTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgrammingLanguageDataTypes_ProgrammingLanguages_ProgrammingLanguageId",
                        column: x => x.ProgrammingLanguageId,
                        principalTable: "ProgrammingLanguages",
                        principalColumn: "ProgrammingLanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_ResultDataTypeId",
                table: "Challenges",
                column: "ResultDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeInputSetupParameters_ChallengeId",
                table: "ChallengeInputSetupParameters",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeInputSetupParameters_DataTypeId",
                table: "ChallengeInputSetupParameters",
                column: "DataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammingLanguageDataTypes_DataTypeId",
                table: "ProgrammingLanguageDataTypes",
                column: "DataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammingLanguageDataTypes_ProgrammingLanguageId",
                table: "ProgrammingLanguageDataTypes",
                column: "ProgrammingLanguageId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Challenges_GeneralDataTypes_ResultDataTypeId",
            //    table: "Challenges",
            //    column: "ResultDataTypeId",
            //    principalTable: "GeneralDataTypes",
            //    principalColumn: "DataTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserChallengeHistories_Challenges_ChallengeId",
                table: "UserChallengeHistories",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "ChallengeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChallengeHistories_Users_UserId",
                table: "UserChallengeHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChallenges_Challenges_ChallengeId",
                table: "UserChallenges",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "ChallengeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChallenges_Users_UserId",
                table: "UserChallenges",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Statuses_StatusId",
                table: "Users",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserScores_Users_UserId",
                table: "UserScores",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_GeneralDataTypes_ResultDataTypeId",
                table: "Challenges");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChallengeHistories_Challenges_ChallengeId",
                table: "UserChallengeHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChallengeHistories_Users_UserId",
                table: "UserChallengeHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChallenges_Challenges_ChallengeId",
                table: "UserChallenges");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChallenges_Users_UserId",
                table: "UserChallenges");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Statuses_StatusId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserScores_Users_UserId",
                table: "UserScores");

            migrationBuilder.DropTable(
                name: "ChallengeInputSetupParameters");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguageDataTypes");

            migrationBuilder.DropTable(
                name: "GeneralDataTypes");

            migrationBuilder.DropIndex(
                name: "IX_Challenges_ResultDataTypeId",
                table: "Challenges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserScores",
                table: "UserScores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserChallenges",
                table: "UserChallenges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserChallengeHistories",
                table: "UserChallengeHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "InputParameters",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "MethodName",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "ResultDataTypeId",
                table: "Challenges");

            migrationBuilder.RenameTable(
                name: "UserScores",
                newName: "UserScore");

            migrationBuilder.RenameTable(
                name: "UserChallenges",
                newName: "UserChallenge");

            migrationBuilder.RenameTable(
                name: "UserChallengeHistories",
                newName: "UserChallengeHistory");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Sequence",
                table: "ChallengeInputParameters",
                newName: "ParameterTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserScores_UserId",
                table: "UserScore",
                newName: "IX_UserScore_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserChallenges_UserId",
                table: "UserChallenge",
                newName: "IX_UserChallenge_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserChallenges_ChallengeId",
                table: "UserChallenge",
                newName: "IX_UserChallenge_ChallengeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserChallengeHistories_UserId",
                table: "UserChallengeHistory",
                newName: "IX_UserChallengeHistory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserChallengeHistories_ChallengeId",
                table: "UserChallengeHistory",
                newName: "IX_UserChallengeHistory_ChallengeId");

            migrationBuilder.AddColumn<int>(
                name: "ParameterTypeId",
                table: "ChallengeValidations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserScore",
                table: "UserScore",
                column: "UserScoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserChallenge",
                table: "UserChallenge",
                column: "UserChallengeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserChallengeHistory",
                table: "UserChallengeHistory",
                column: "UserChallengeHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "StatusId");

            migrationBuilder.CreateTable(
                name: "ParameterTypes",
                columns: table => new
                {
                    ParameterTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterTypes", x => x.ParameterTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeValidations_ParameterTypeId",
                table: "ChallengeValidations",
                column: "ParameterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeInputParameters_ParameterTypeId",
                table: "ChallengeInputParameters",
                column: "ParameterTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChallengeInputParameters_ParameterTypes_ParameterTypeId",
                table: "ChallengeInputParameters",
                column: "ParameterTypeId",
                principalTable: "ParameterTypes",
                principalColumn: "ParameterTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChallengeValidations_ParameterTypes_ParameterTypeId",
                table: "ChallengeValidations",
                column: "ParameterTypeId",
                principalTable: "ParameterTypes",
                principalColumn: "ParameterTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChallenge_Challenges_ChallengeId",
                table: "UserChallenge",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "ChallengeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChallenge_Users_UserId",
                table: "UserChallenge",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChallengeHistory_Challenges_ChallengeId",
                table: "UserChallengeHistory",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "ChallengeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChallengeHistory_Users_UserId",
                table: "UserChallengeHistory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Status_StatusId",
                table: "Users",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserScore_Users_UserId",
                table: "UserScore",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
