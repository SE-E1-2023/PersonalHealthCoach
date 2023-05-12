﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCoach.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration_20230512_123328 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DietPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Scope = table.Column<string>(type: "text", nullable: false),
                    DietType = table.Column<List<string>>(type: "text[]", nullable: false),
                    Recommandations = table.Column<List<string>>(type: "text[]", nullable: false),
                    Interdictions = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FitnessPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitnessPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    MedicalHistory = table.Column<List<string>>(type: "text[]", nullable: false),
                    CurrentIllnesses = table.Column<List<string>>(type: "text[]", nullable: false),
                    Goal = table.Column<string>(type: "text", nullable: false),
                    UnwantedExercises = table.Column<List<string>>(type: "text[]", nullable: false),
                    DailySteps = table.Column<int>(type: "integer", nullable: true),
                    HoursOfSleep = table.Column<double>(type: "double precision", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalTip",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    TipText = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalTip", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Target = table.Column<string>(type: "text", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    ReportedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SolvedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    HasElevatedRights = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WellnessTip",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TipText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WellnessTip", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompletedExercise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CaloriesBurned = table.Column<int>(type: "integer", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "integer", nullable: false),
                    IsNew = table.Column<bool>(type: "boolean", nullable: false),
                    ExerciseHistoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedExercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedExercise_ExerciseHistory_ExerciseHistoryId",
                        column: x => x.ExerciseHistoryId,
                        principalTable: "ExerciseHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    RepRange = table.Column<string>(type: "text", nullable: true),
                    RestTime = table.Column<string>(type: "text", nullable: true),
                    Sets = table.Column<int>(type: "integer", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    FitnessPlanId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercise_FitnessPlan_FitnessPlanId",
                        column: x => x.FitnessPlanId,
                        principalTable: "FitnessPlan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConsumedFood",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    ConsumedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Calories = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    IsNew = table.Column<bool>(type: "boolean", nullable: false),
                    FoodHistoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumedFood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumedFood_FoodHistory_FoodHistoryId",
                        column: x => x.FoodHistoryId,
                        principalTable: "FoodHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedExercise_ExerciseHistoryId",
                table: "CompletedExercise",
                column: "ExerciseHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumedFood_FoodHistoryId",
                table: "ConsumedFood",
                column: "FoodHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_FitnessPlanId",
                table: "Exercise",
                column: "FitnessPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedExercise");

            migrationBuilder.DropTable(
                name: "ConsumedFood");

            migrationBuilder.DropTable(
                name: "DietPlan");

            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "PersonalData");

            migrationBuilder.DropTable(
                name: "PersonalTip");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "WellnessTip");

            migrationBuilder.DropTable(
                name: "ExerciseHistory");

            migrationBuilder.DropTable(
                name: "FoodHistory");

            migrationBuilder.DropTable(
                name: "FitnessPlan");
        }
    }
}