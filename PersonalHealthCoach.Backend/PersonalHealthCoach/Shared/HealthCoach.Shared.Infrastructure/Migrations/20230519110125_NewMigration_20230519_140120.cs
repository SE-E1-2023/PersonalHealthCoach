using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCoach.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration_20230519_140120 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBands",
                table: "PersonalData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBarbell",
                table: "PersonalData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasCable",
                table: "PersonalData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDumbbell",
                table: "PersonalData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasEasyCurlBar",
                table: "PersonalData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasExerciseBall",
                table: "PersonalData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasFoamRoll",
                table: "PersonalData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasKettlebells",
                table: "PersonalData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasMachine",
                table: "PersonalData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasMedicineBall",
                table: "PersonalData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasNone",
                table: "PersonalData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasOther",
                table: "PersonalData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProUser",
                table: "PersonalData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WantsBodyOnly",
                table: "PersonalData",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutsPerWeek",
                table: "PersonalData",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBands",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "HasBarbell",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "HasCable",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "HasDumbbell",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "HasEasyCurlBar",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "HasExerciseBall",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "HasFoamRoll",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "HasKettlebells",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "HasMachine",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "HasMedicineBall",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "HasNone",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "HasOther",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "IsProUser",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "WantsBodyOnly",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "WorkoutsPerWeek",
                table: "PersonalData");
        }
    }
}
