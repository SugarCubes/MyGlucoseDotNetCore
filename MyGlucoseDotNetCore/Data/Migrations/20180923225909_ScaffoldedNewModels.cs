using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyGlucoseDotNetCore.Data.Migrations
{
    public partial class ScaffoldedNewModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ExerciseEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ExerciseName = table.Column<string>(nullable: true),
                    Minutes = table.Column<int>(nullable: false),
                    PatientId = table.Column<string>(nullable: true),
                    Timestamp = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseEntry_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExerciseEntry_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GlucoseEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BeforeAfter = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Measurement = table.Column<float>(nullable: false),
                    PatientId = table.Column<string>(nullable: true),
                    PatientUsername = table.Column<string>(nullable: true),
                    Timestamp = table.Column<long>(nullable: false),
                    WhichMeal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlucoseEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlucoseEntry_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MealEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Timestamp = table.Column<long>(nullable: false),
                    TotalCarbs = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealEntry_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MealItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Carbs = table.Column<int>(nullable: false),
                    MealId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Servings = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealItem_MealEntry_MealId",
                        column: x => x.MealId,
                        principalTable: "MealEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseEntry_PatientId",
                table: "ExerciseEntry",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseEntry_UserId",
                table: "ExerciseEntry",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GlucoseEntry_PatientId",
                table: "GlucoseEntry",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MealEntry_UserId",
                table: "MealEntry",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MealItem_MealId",
                table: "MealItem",
                column: "MealId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseEntry");

            migrationBuilder.DropTable(
                name: "GlucoseEntry");

            migrationBuilder.DropTable(
                name: "MealItem");

            migrationBuilder.DropTable(
                name: "MealEntry");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
