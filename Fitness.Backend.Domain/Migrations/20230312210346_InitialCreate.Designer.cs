﻿// <auto-generated />
using System;
using Fitness.Backend.Domain.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fitness.Backend.Domain.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230312210346_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Fitness.Backend.Application.DataContracts.Models.Entity.Image", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Del")
                        .HasColumnType("integer");

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ProfilePictures");
                });

            modelBuilder.Entity("Fitness.Backend.Application.DataContracts.Models.Entity.Instructor", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Del")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("Fitness.Backend.Application.DataContracts.Models.Entity.Lesson", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Day")
                        .HasColumnType("integer");

                    b.Property<int>("Del")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("InstructorId")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<int?>("MaxNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("SportId")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.HasIndex("SportId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Fitness.Backend.Application.DataContracts.Models.Entity.Sport", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Del")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("Fitness.Backend.Application.DataContracts.Models.Entity.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Del")
                        .HasColumnType("integer");

                    b.Property<int?>("ImageId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("ProfilePicId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProfilePicId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("InstructorSport", b =>
                {
                    b.Property<string>("InstructorsId")
                        .HasColumnType("text");

                    b.Property<string>("SportsId")
                        .HasColumnType("text");

                    b.HasKey("InstructorsId", "SportsId");

                    b.HasIndex("SportsId");

                    b.ToTable("InstructorSport");
                });

            modelBuilder.Entity("LessonUser", b =>
                {
                    b.Property<string>("LessonsId")
                        .HasColumnType("text");

                    b.Property<string>("UsersId")
                        .HasColumnType("text");

                    b.HasKey("LessonsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("LessonUser");
                });

            modelBuilder.Entity("Fitness.Backend.Application.DataContracts.Models.Entity.Instructor", b =>
                {
                    b.HasOne("Fitness.Backend.Application.DataContracts.Models.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fitness.Backend.Application.DataContracts.Models.Entity.Lesson", b =>
                {
                    b.HasOne("Fitness.Backend.Application.DataContracts.Models.Entity.Instructor", "Instructor")
                        .WithMany("Lessons")
                        .HasForeignKey("InstructorId");

                    b.HasOne("Fitness.Backend.Application.DataContracts.Models.Entity.Sport", "Sport")
                        .WithMany()
                        .HasForeignKey("SportId");

                    b.Navigation("Instructor");

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("Fitness.Backend.Application.DataContracts.Models.Entity.User", b =>
                {
                    b.HasOne("Fitness.Backend.Application.DataContracts.Models.Entity.Image", "ProfilePic")
                        .WithMany()
                        .HasForeignKey("ProfilePicId");

                    b.Navigation("ProfilePic");
                });

            modelBuilder.Entity("InstructorSport", b =>
                {
                    b.HasOne("Fitness.Backend.Application.DataContracts.Models.Entity.Instructor", null)
                        .WithMany()
                        .HasForeignKey("InstructorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fitness.Backend.Application.DataContracts.Models.Entity.Sport", null)
                        .WithMany()
                        .HasForeignKey("SportsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LessonUser", b =>
                {
                    b.HasOne("Fitness.Backend.Application.DataContracts.Models.Entity.Lesson", null)
                        .WithMany()
                        .HasForeignKey("LessonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fitness.Backend.Application.DataContracts.Models.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fitness.Backend.Application.DataContracts.Models.Entity.Instructor", b =>
                {
                    b.Navigation("Lessons");
                });
#pragma warning restore 612, 618
        }
    }
}
