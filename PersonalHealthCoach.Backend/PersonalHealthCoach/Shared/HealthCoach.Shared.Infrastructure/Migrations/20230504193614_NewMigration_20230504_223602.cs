using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCoach.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration_20230504_223602 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasElevatedRights",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasElevatedRights",
                table: "User");
        }
    }
}
