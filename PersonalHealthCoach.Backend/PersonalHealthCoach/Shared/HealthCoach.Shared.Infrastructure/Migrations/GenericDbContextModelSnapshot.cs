﻿// <auto-generated />

#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

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

            modelBuilder.Entity("HealthCoach.Core.Domain.DietPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<List<string>>("DietType")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<List<string>>("Interdictions")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("Recommandations")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Scope")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DietPlan");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("FitnessPlanId")
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

                    b.HasKey("Id");

                    b.HasIndex("FitnessPlanId");

                    b.ToTable("Exercise");
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

            modelBuilder.Entity("HealthCoach.Core.Domain.Exercise", b =>
                {
                    b.HasOne("HealthCoach.Core.Domain.FitnessPlan", null)
                        .WithMany("Exercises")
                        .HasForeignKey("FitnessPlanId");
                });

            modelBuilder.Entity("HealthCoach.Core.Domain.FitnessPlan", b =>
                {
                    b.Navigation("Exercises");
                });
#pragma warning restore 612, 618
        }
    }
}
