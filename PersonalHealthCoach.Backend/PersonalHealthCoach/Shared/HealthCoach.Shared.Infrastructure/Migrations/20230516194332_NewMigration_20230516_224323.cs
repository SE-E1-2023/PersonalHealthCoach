using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCoach.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration_20230516_224323 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImportanceLevel",
                table: "PersonalTip");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImportanceLevel",
                table: "PersonalTip",
                type: "text",
                nullable: true);
        }
    }
}
