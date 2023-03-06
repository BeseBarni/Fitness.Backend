using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fitness.Backend.Domain.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class cascadeNoAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructors_Clients_UserId",
                        column: x => x.UserId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InstructorSport",
                columns: table => new
                {
                    InstructorsId = table.Column<int>(type: "int", nullable: false),
                    SportsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorSport", x => new { x.InstructorsId, x.SportsId });
                    table.ForeignKey(
                        name: "FK_InstructorSport_Instructors_InstructorsId",
                        column: x => x.InstructorsId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorSport_Sports_SportsId",
                        column: x => x.SportsId,
                        principalTable: "Sports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    MaxNumber = table.Column<int>(type: "int", nullable: false),
                    SportId = table.Column<int>(type: "int", nullable: true),
                    InstructorId = table.Column<int>(type: "int", nullable: true),
                    Day = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lessons_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lessons_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LessonUser",
                columns: table => new
                {
                    LessonsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonUser", x => new { x.LessonsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_LessonUser_Clients_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonUser_Lessons_LessonsId",
                        column: x => x.LessonsId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "PostalCode" },
                values: new object[,]
                {
                    { 1, "Budapest", 1011 },
                    { 2, "Debrecen", 4024 },
                    { 3, "Miskolc", 3525 },
                    { 4, "Szeged", 6720 },
                    { 5, "Pécs", 7621 },
                    { 6, "Győr", 9021 },
                    { 7, "Nyíregyháza", 4400 },
                    { 8, "Kecskemét", 6000 },
                    { 9, "Székesfehérvár", 8000 },
                    { 10, "Szombathely", 9700 },
                    { 11, "Szolnok", 5000 },
                    { 12, "Tatabánya", 2800 },
                    { 13, "Kaposvár", 7400 },
                    { 14, "Érd", 2030 },
                    { 15, "Veszprém", 8200 },
                    { 16, "Békéscsaba", 5600 },
                    { 17, "Zalaegerszeg", 8900 },
                    { 18, "Sopron", 9400 },
                    { 19, "Eger", 3300 },
                    { 20, "Nagykanizsa", 8800 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CityId",
                table: "Clients",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_UserId",
                table: "Instructors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorSport_SportsId",
                table: "InstructorSport",
                column: "SportsId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CityId",
                table: "Lessons",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_InstructorId",
                table: "Lessons",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SportId",
                table: "Lessons",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUser_UsersId",
                table: "LessonUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstructorSport");

            migrationBuilder.DropTable(
                name: "LessonUser");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Sports");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
