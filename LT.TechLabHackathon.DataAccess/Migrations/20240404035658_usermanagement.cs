using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LT.TechLabHackathon.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Points",
                table: "ChallengeLevels",
                newName: "Score");

            migrationBuilder.AddColumn<int>(
                name: "NormalTime",
                table: "ChallengeLevels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthDoublefactor = table.Column<bool>(type: "bit", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthUserKeys",
                columns: table => new
                {
                    AuthUserKeyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    KeyCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KeyExpiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DynamicKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUserKeys", x => x.AuthUserKeyId);
                    table.ForeignKey(
                        name: "FK_AuthUserKeys_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserChallenge",
                columns: table => new
                {
                    UserChallengeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    Unlocked = table.Column<bool>(type: "bit", nullable: false),
                    UnlockedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UnlokedTime = table.Column<int>(type: "int", nullable: false),
                    Attemps = table.Column<int>(type: "int", nullable: false),
                    Penalized = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChallenge", x => x.UserChallengeId);
                    table.ForeignKey(
                        name: "FK_UserChallenge_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "ChallengeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChallenge_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserChallengeHistory",
                columns: table => new
                {
                    UserChallengeHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Unlocked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChallengeHistory", x => x.UserChallengeHistoryId);
                    table.ForeignKey(
                        name: "FK_UserChallengeHistory_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "ChallengeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChallengeHistory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserScore",
                columns: table => new
                {
                    UserScoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    UnlockedChallenges = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScore", x => x.UserScoreId);
                    table.ForeignKey(
                        name: "FK_UserScore_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthUserKeys_UserId",
                table: "AuthUserKeys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChallenge_ChallengeId",
                table: "UserChallenge",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChallenge_UserId",
                table: "UserChallenge",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChallengeHistory_ChallengeId",
                table: "UserChallengeHistory",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChallengeHistory_UserId",
                table: "UserChallengeHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatusId",
                table: "Users",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserScore_UserId",
                table: "UserScore",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthUserKeys");

            migrationBuilder.DropTable(
                name: "UserChallenge");

            migrationBuilder.DropTable(
                name: "UserChallengeHistory");

            migrationBuilder.DropTable(
                name: "UserScore");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropColumn(
                name: "NormalTime",
                table: "ChallengeLevels");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "ChallengeLevels",
                newName: "Points");
        }
    }
}
