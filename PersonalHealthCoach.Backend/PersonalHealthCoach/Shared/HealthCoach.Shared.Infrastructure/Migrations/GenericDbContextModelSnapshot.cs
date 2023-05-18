﻿// <auto-generated />
using System;
using System.Collections.Generic;
using HealthCoach.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HealthCoach.Shared.Infrastructure.Migrations
{
    [DbContext(typeof(GenericDbContext))]
    partial class GenericDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HealthCoach.Core.Domain.CompletedExercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CaloriesBurned")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CompletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DurationInMinutes")
                        .HasColumnType("integer");

                    b.Property<Guid?>("ExerciseHistoryId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsNew")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseHistoryId");

                    b.ToTable("CompletedExercise");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.ConsumedFood", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Calories")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ConsumedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("FoodHistoryId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsNew")
                        .HasColumnType("boolean");

                    b.Property<string>("Meal")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FoodHistoryId");

                    b.ToTable("ConsumedFood");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.DietPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BreakfastId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<List<string>>("DietType")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<Guid>("DrinkId")
                        .HasColumnType("uuid");

                    b.Property<List<string>>("Interdictions")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<Guid>("MainCourseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("Recommendations")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Scope")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SideDishId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SnackId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SoupId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BreakfastId");

                    b.HasIndex("DrinkId");

                    b.HasIndex("MainCourseId");

                    b.HasIndex("SideDishId");

                    b.HasIndex("SnackId");

                    b.HasIndex("SoupId");

                    b.ToTable("DietPlan");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("RepRange")
                        .HasColumnType("text");

                    b.Property<string>("RestTime")
                        .HasColumnType("text");

                    b.Property<int?>("Sets")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<Guid?>("WorkoutId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.ExerciseHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("ExerciseHistory");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.FitnessPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("FitnessPlan");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.FoodHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("FoodHistory");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.Meal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Calories")
                        .HasColumnType("double precision");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("Ingredients")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Meal");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.PersonalData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<List<string>>("CurrentIllnesses")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<int?>("DailySteps")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Goal")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<double?>("HoursOfSleep")
                        .HasColumnType("double precision");

                    b.Property<List<string>>("MedicalHistory")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<List<string>>("UnwantedExercises")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("PersonalData");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.PersonalTip", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("TipText")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("PersonalTip");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.Report", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ReportedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("SolvedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Target")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TargetId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("HasElevatedRights")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.WellnessTip", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("TipText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WellnessTip");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.Workout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("FitnessPlanId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FitnessPlanId");

                    b.ToTable("Workout");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.CompletedExercise", b =>
                {
                    b.HasOne("HealthCoach.Core.Domain.ExerciseHistory", null)
                        .WithMany("CompletedExercises")
                        .HasForeignKey("ExerciseHistoryId");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.ConsumedFood", b =>
                {
                    b.HasOne("HealthCoach.Core.Domain.FoodHistory", null)
                        .WithMany("ConsumedFoods")
                        .HasForeignKey("FoodHistoryId");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.DietPlan", b =>
                {
                    b.HasOne("HealthCoach.Core.Domain.Meal", "Breakfast")
                        .WithMany()
                        .HasForeignKey("BreakfastId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthCoach.Core.Domain.Meal", "Drink")
                        .WithMany()
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthCoach.Core.Domain.Meal", "MainCourse")
                        .WithMany()
                        .HasForeignKey("MainCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthCoach.Core.Domain.Meal", "SideDish")
                        .WithMany()
                        .HasForeignKey("SideDishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthCoach.Core.Domain.Meal", "Snack")
                        .WithMany()
                        .HasForeignKey("SnackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthCoach.Core.Domain.Meal", "Soup")
                        .WithMany()
                        .HasForeignKey("SoupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Breakfast");

                    b.Navigation("Drink");

                    b.Navigation("MainCourse");

                    b.Navigation("SideDish");

                    b.Navigation("Snack");

                    b.Navigation("Soup");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.Exercise", b =>
                {
                    b.HasOne("HealthCoach.Core.Domain.Workout", null)
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutId");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.Workout", b =>
                {
                    b.HasOne("HealthCoach.Core.Domain.FitnessPlan", null)
                        .WithMany("Workouts")
                        .HasForeignKey("FitnessPlanId");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.ExerciseHistory", b =>
                {
                    b.Navigation("CompletedExercises");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.FitnessPlan", b =>
                {
                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.FoodHistory", b =>
                {
                    b.Navigation("ConsumedFoods");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.Workout", b =>
                {
                    b.Navigation("Exercises");
                });
#pragma warning restore 612, 618
        }
    }
}
