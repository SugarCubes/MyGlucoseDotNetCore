using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyGlucoseDotNetCore.Migrations
{
    public partial class patients2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseEntries_AspNetUsers_PatientId",
                table: "ExerciseEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseEntries_AspNetUsers_UserId",
                table: "ExerciseEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseEntries_MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel_UserName",
                table: "ExerciseEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_GlucoseEntry_AspNetUsers_PatientId",
                table: "GlucoseEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_GlucoseEntry_MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel_PatientViewModelUserName",
                table: "GlucoseEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_MealEntries_AspNetUsers_PatientId",
                table: "MealEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_MealEntries_AspNetUsers_UserId",
                table: "MealEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_MealEntries_MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel_UserName",
                table: "MealEntries");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel_TempId",
                table: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel_TempId1",
                table: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel_TempId2",
                table: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MyGlucoseDotNetCore.Models.Doctor_TempId",
                table: "MyGlucoseDotNetCore.Models.Doctor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlucoseEntry",
                table: "GlucoseEntry");

            migrationBuilder.DropColumn(
                name: "TempId",
                table: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel");

            migrationBuilder.DropColumn(
                name: "TempId1",
                table: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel");

            migrationBuilder.DropColumn(
                name: "TempId2",
                table: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel");

            migrationBuilder.RenameTable(
                name: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel",
                newName: "PatientViewModel");

            migrationBuilder.RenameTable(
                name: "MyGlucoseDotNetCore.Models.Doctor",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "GlucoseEntry",
                newName: "GlucoseEntries");

            migrationBuilder.RenameColumn(
                name: "TempId",
                table: "AspNetUsers",
                newName: "Discriminator");

            migrationBuilder.RenameIndex(
                name: "IX_GlucoseEntry_PatientId",
                table: "GlucoseEntries",
                newName: "IX_GlucoseEntries_PatientId");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "PatientViewModel",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "PatientViewModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "PatientViewModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "PatientViewModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PatientViewModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "PatientViewModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "PatientViewModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "PatientViewModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "PatientViewModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Zip1",
                table: "PatientViewModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Zip2",
                table: "PatientViewModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "RemoteLoginExpiration",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "RemoteLoginToken",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Zip1",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Zip2",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DegreeAbbreviation",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientViewModel",
                table: "PatientViewModel",
                column: "UserName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlucoseEntries",
                table: "GlucoseEntries",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DoctorId",
                table: "AspNetUsers",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_GlucoseEntries_PatientViewModelUserName",
                table: "GlucoseEntries",
                column: "PatientViewModelUserName");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_DoctorId",
                table: "AspNetUsers",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseEntries_AspNetUsers_PatientId",
                table: "ExerciseEntries",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseEntries_AspNetUsers_UserId",
                table: "ExerciseEntries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_MealEntries_AspNetUsers_PatientId",
                table: "MealEntries",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MealEntries_AspNetUsers_UserId",
                table: "MealEntries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
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
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_DoctorId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseEntries_AspNetUsers_PatientId",
                table: "ExerciseEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseEntries_AspNetUsers_UserId",
                table: "ExerciseEntries");

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
                name: "FK_MealEntries_AspNetUsers_PatientId",
                table: "MealEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_MealEntries_AspNetUsers_UserId",
                table: "MealEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_MealEntries_PatientViewModel_UserName",
                table: "MealEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientViewModel",
                table: "PatientViewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlucoseEntries",
                table: "GlucoseEntries");

            migrationBuilder.DropIndex(
                name: "IX_GlucoseEntries_PatientViewModelUserName",
                table: "GlucoseEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DoctorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "PatientViewModel");

            migrationBuilder.DropColumn(
                name: "Address1",
                table: "PatientViewModel");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "PatientViewModel");

            migrationBuilder.DropColumn(
                name: "City",
                table: "PatientViewModel");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PatientViewModel");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PatientViewModel");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "PatientViewModel");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PatientViewModel");

            migrationBuilder.DropColumn(
                name: "State",
                table: "PatientViewModel");

            migrationBuilder.DropColumn(
                name: "Zip1",
                table: "PatientViewModel");

            migrationBuilder.DropColumn(
                name: "Zip2",
                table: "PatientViewModel");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Address1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RemoteLoginExpiration",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RemoteLoginToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Zip1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Zip2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DegreeAbbreviation",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "PatientViewModel",
                newName: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel");

            migrationBuilder.RenameTable(
                name: "GlucoseEntries",
                newName: "GlucoseEntry");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "MyGlucoseDotNetCore.Models.Doctor");

            migrationBuilder.RenameIndex(
                name: "IX_GlucoseEntries_PatientId",
                table: "GlucoseEntry",
                newName: "IX_GlucoseEntry_PatientId");

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "MyGlucoseDotNetCore.Models.Doctor",
                newName: "TempId");

            migrationBuilder.AddColumn<string>(
                name: "TempId",
                table: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TempId1",
                table: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TempId2",
                table: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "TempId",
                table: "MyGlucoseDotNetCore.Models.Doctor",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel_TempId",
                table: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel",
                column: "TempId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel_TempId1",
                table: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel",
                column: "TempId1");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel_TempId2",
                table: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel",
                column: "TempId2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlucoseEntry",
                table: "GlucoseEntry",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MyGlucoseDotNetCore.Models.Doctor_TempId",
                table: "MyGlucoseDotNetCore.Models.Doctor",
                column: "TempId");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    RemoteLoginExpiration = table.Column<long>(nullable: false),
                    RemoteLoginToken = table.Column<Guid>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    Zip1 = table.Column<int>(nullable: false),
                    Zip2 = table.Column<int>(nullable: false),
                    DoctorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_MyGlucoseDotNetCore.Models.Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "MyGlucoseDotNetCore.Models.Doctor",
                        principalColumn: "TempId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DoctorId",
                table: "AspNetUsers",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseEntries_AspNetUsers_PatientId",
                table: "ExerciseEntries",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseEntries_AspNetUsers_UserId",
                table: "ExerciseEntries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseEntries_MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel_UserName",
                table: "ExerciseEntries",
                column: "UserName",
                principalTable: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GlucoseEntry_AspNetUsers_PatientId",
                table: "GlucoseEntry",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GlucoseEntry_MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel_PatientViewModelUserName",
                table: "GlucoseEntry",
                column: "PatientViewModelUserName",
                principalTable: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel",
                principalColumn: "TempId1",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MealEntries_AspNetUsers_PatientId",
                table: "MealEntries",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MealEntries_AspNetUsers_UserId",
                table: "MealEntries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MealEntries_MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel_UserName",
                table: "MealEntries",
                column: "UserName",
                principalTable: "MyGlucoseDotNetCore.Models.ViewModels.PatientViewModel",
                principalColumn: "TempId2",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
