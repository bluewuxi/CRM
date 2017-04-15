using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.WebUI.Data
{
    public partial class signature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisaApplications_AspNetUsers_CreatedById",
                table: "VisaApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaApplications_AspNetUsers_ModifiedById",
                table: "VisaApplications");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "VisaApplications",
                newName: "ModifiedByID");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "VisaApplications",
                newName: "CreatedByID");

            migrationBuilder.RenameIndex(
                name: "IX_VisaApplications_ModifiedById",
                table: "VisaApplications",
                newName: "IX_VisaApplications_ModifiedByID");

            migrationBuilder.RenameIndex(
                name: "IX_VisaApplications_CreatedById",
                table: "VisaApplications",
                newName: "IX_VisaApplications_CreatedByID");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByID",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Activities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByID",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedTime",
                table: "Activities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CreatedByID",
                table: "Activities",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ModifiedByID",
                table: "Activities",
                column: "ModifiedByID");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_AspNetUsers_CreatedByID",
                table: "Activities",
                column: "CreatedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_AspNetUsers_ModifiedByID",
                table: "Activities",
                column: "ModifiedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaApplications_AspNetUsers_CreatedByID",
                table: "VisaApplications",
                column: "CreatedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaApplications_AspNetUsers_ModifiedByID",
                table: "VisaApplications",
                column: "ModifiedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_AspNetUsers_CreatedByID",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_AspNetUsers_ModifiedByID",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaApplications_AspNetUsers_CreatedByID",
                table: "VisaApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaApplications_AspNetUsers_ModifiedByID",
                table: "VisaApplications");

            migrationBuilder.DropIndex(
                name: "IX_Activities_CreatedByID",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_ModifiedByID",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "CreatedByID",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ModifiedByID",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ModifiedTime",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "ModifiedByID",
                table: "VisaApplications",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "CreatedByID",
                table: "VisaApplications",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_VisaApplications_ModifiedByID",
                table: "VisaApplications",
                newName: "IX_VisaApplications_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_VisaApplications_CreatedByID",
                table: "VisaApplications",
                newName: "IX_VisaApplications_CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_VisaApplications_AspNetUsers_CreatedById",
                table: "VisaApplications",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaApplications_AspNetUsers_ModifiedById",
                table: "VisaApplications",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
