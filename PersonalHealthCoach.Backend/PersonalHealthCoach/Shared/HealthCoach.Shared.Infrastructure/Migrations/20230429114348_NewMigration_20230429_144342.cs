using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCoach.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration_20230429_144342 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DailySteps",
                table: "PersonalData",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "PersonalData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "HoursOfSleep",
                table: "PersonalData",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailySteps",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "HoursOfSleep",
                table: "PersonalData");
        }
    }
}
