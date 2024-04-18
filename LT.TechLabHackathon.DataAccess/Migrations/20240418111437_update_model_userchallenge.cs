using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LT.TechLabHackathon.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class update_model_userchallenge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeChallenge",
                table: "UserChallenges",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodeChallenge",
                table: "UserChallengeHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeChallenge",
                table: "UserChallenges");

            migrationBuilder.DropColumn(
                name: "CodeChallenge",
                table: "UserChallengeHistories");
        }
    }
}
