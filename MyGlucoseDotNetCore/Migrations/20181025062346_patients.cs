using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyGlucoseDotNetCore.Migrations
{
    public partial class patients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlucoseEntry_AspNetUsers_PatientId",
                table: "GlucoseEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlucoseEntry",
                table: "GlucoseEntry");

            migrationBuilder.RenameTable(
                name: "GlucoseEntry",
                newName: "GlucoseEntries");

            migrationBuilder.RenameIndex(
                name: "IX_GlucoseEntry_PatientId",
                table: "GlucoseEntries",
                newName: "IX_GlucoseEntries_PatientId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "MealEntries",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientViewModelUserName",
                table: "GlucoseEntries",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ExerciseEntries",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlucoseEntries",
                table: "GlucoseEntries",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PatientViewModel",
                columns: table => new
                {
                    UserName = table.Column<string>(nullable: false),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip1 = table.Column<int>(nullable: false),
                    Zip2 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientViewModel", x => x.UserName);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealEntries_UserName",
                table: "MealEntries",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_GlucoseEntries_PatientViewModelUserName",
                table: "GlucoseEntries",
                column: "PatientViewModelUserName");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseEntries_UserName",
                table: "ExerciseEntries",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseEntries_PatientViewModel_UserName",
                table: "ExerciseEntries",
                column: "UserName",
                principalTable: "PatientViewModel",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GlucoseEntries_AspNetUsers_PatientId",
                table: "GlucoseEntries",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GlucoseEntries_PatientViewModel_PatientViewModelUserName",
                table: "GlucoseEntries",
                column: "PatientViewModelUserName",
                principalTable: "PatientViewModel",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MealEntries_PatientViewModel_UserName",
                table: "MealEntries",
                column: "UserName",
                principalTable: "PatientViewModel",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseEntries_PatientViewModel_UserName",
                table: "ExerciseEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_GlucoseEntries_AspNetUsers_PatientId",
                table: "GlucoseEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_GlucoseEntries_PatientViewModel_PatientViewModelUserName",
                table: "GlucoseEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_MealEntries_PatientViewModel_UserName",
                table: "MealEntries");

            migrationBuilder.DropTable(
                name: "PatientViewModel");

            migrationBuilder.DropIndex(
                name: "IX_MealEntries_UserName",
                table: "MealEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlucoseEntries",
                table: "GlucoseEntries");

            migrationBuilder.DropIndex(
                name: "IX_GlucoseEntries_PatientViewModelUserName",
                table: "GlucoseEntries");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseEntries_UserName",
                table: "ExerciseEntries");

            migrationBuilder.DropColumn(
                name: "PatientViewModelUserName",
                table: "GlucoseEntries");

            migrationBuilder.RenameTable(
                name: "GlucoseEntries",
                newName: "GlucoseEntry");

            migrationBuilder.RenameIndex(
                name: "IX_GlucoseEntries_PatientId",
                table: "GlucoseEntry",
                newName: "IX_GlucoseEntry_PatientId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "MealEntries",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ExerciseEntries",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlucoseEntry",
                table: "GlucoseEntry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GlucoseEntry_AspNetUsers_PatientId",
                table: "GlucoseEntry",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
