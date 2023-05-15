using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCoach.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration_20230514_184020 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BreakfastId",
                table: "DietPlan",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DrinkId",
                table: "DietPlan",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MainCourseId",
                table: "DietPlan",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SideDishId",
                table: "DietPlan",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SnackId",
                table: "DietPlan",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SoupId",
                table: "DietPlan",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Meal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Ingredients = table.Column<List<string>>(type: "text[]", nullable: false),
                    Calories = table.Column<double>(type: "double precision", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietPlan_BreakfastId",
                table: "DietPlan",
                column: "BreakfastId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPlan_DrinkId",
                table: "DietPlan",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPlan_MainCourseId",
                table: "DietPlan",
                column: "MainCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPlan_SideDishId",
                table: "DietPlan",
                column: "SideDishId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPlan_SnackId",
                table: "DietPlan",
                column: "SnackId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPlan_SoupId",
                table: "DietPlan",
                column: "SoupId");

            migrationBuilder.AddForeignKey(
                name: "FK_DietPlan_Meal_BreakfastId",
                table: "DietPlan",
                column: "BreakfastId",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DietPlan_Meal_DrinkId",
                table: "DietPlan",
                column: "DrinkId",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DietPlan_Meal_MainCourseId",
                table: "DietPlan",
                column: "MainCourseId",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DietPlan_Meal_SideDishId",
                table: "DietPlan",
                column: "SideDishId",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DietPlan_Meal_SnackId",
                table: "DietPlan",
                column: "SnackId",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DietPlan_Meal_SoupId",
                table: "DietPlan",
                column: "SoupId",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietPlan_Meal_BreakfastId",
                table: "DietPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_DietPlan_Meal_DrinkId",
                table: "DietPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_DietPlan_Meal_MainCourseId",
                table: "DietPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_DietPlan_Meal_SideDishId",
                table: "DietPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_DietPlan_Meal_SnackId",
                table: "DietPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_DietPlan_Meal_SoupId",
                table: "DietPlan");

            migrationBuilder.DropTable(
                name: "Meal");

            migrationBuilder.DropIndex(
                name: "IX_DietPlan_BreakfastId",
                table: "DietPlan");

            migrationBuilder.DropIndex(
                name: "IX_DietPlan_DrinkId",
                table: "DietPlan");

            migrationBuilder.DropIndex(
                name: "IX_DietPlan_MainCourseId",
                table: "DietPlan");

            migrationBuilder.DropIndex(
                name: "IX_DietPlan_SideDishId",
                table: "DietPlan");

            migrationBuilder.DropIndex(
                name: "IX_DietPlan_SnackId",
                table: "DietPlan");

            migrationBuilder.DropIndex(
                name: "IX_DietPlan_SoupId",
                table: "DietPlan");

            migrationBuilder.DropColumn(
                name: "BreakfastId",
                table: "DietPlan");

            migrationBuilder.DropColumn(
                name: "DrinkId",
                table: "DietPlan");

            migrationBuilder.DropColumn(
                name: "MainCourseId",
                table: "DietPlan");

            migrationBuilder.DropColumn(
                name: "SideDishId",
                table: "DietPlan");

            migrationBuilder.DropColumn(
                name: "SnackId",
                table: "DietPlan");

            migrationBuilder.DropColumn(
                name: "SoupId",
                table: "DietPlan");
        }
    }
}
