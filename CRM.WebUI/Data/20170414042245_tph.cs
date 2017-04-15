using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.WebUI.Data
{
    public partial class tph : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_AccountOwnerId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_ModifiedById",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Students_AttendStudentStudentID",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Accounts_EnrollmentAgentAccountID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Accounts_InstituteAccountID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Accounts_AgentAccountID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_ModifiedById",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_StudentOwnerId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaApplications_Students_StudentID",
                table: "VisaApplications");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_EnrollmentAgentAccountID",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "ClientNumber",
                table: "VisaApplications");

            migrationBuilder.DropColumn(
                name: "EnrollmentAgentAccountID",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "Students",
                newName: "ModifiedByID");

            migrationBuilder.RenameColumn(
                name: "StudentOwnerId",
                table: "Students",
                newName: "CustomerOwnerID");

            migrationBuilder.RenameColumn(
                name: "RegisterDate",
                table: "Students",
                newName: "ModifiedTime");

            migrationBuilder.RenameColumn(
                name: "AgentAccountID",
                table: "Students",
                newName: "AgentID");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "Students",
                newName: "CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Students_ModifiedById",
                table: "Students",
                newName: "IX_Students_ModifiedByID");

            migrationBuilder.RenameIndex(
                name: "IX_Students_StudentOwnerId",
                table: "Students",
                newName: "IX_Students_CustomerOwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_Students_AgentAccountID",
                table: "Students",
                newName: "IX_Students_AgentID");

            migrationBuilder.RenameColumn(
                name: "InstituteAccountID",
                table: "Enrollments",
                newName: "EnrollmentAgentID");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_InstituteAccountID",
                table: "Enrollments",
                newName: "IX_Enrollments_EnrollmentAgentID");

            migrationBuilder.RenameColumn(
                name: "AttendStudentStudentID",
                table: "Activities",
                newName: "AttendStudentCustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_AttendStudentStudentID",
                table: "Activities",
                newName: "IX_Activities_AttendStudentCustomerID");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "Accounts",
                newName: "ModifiedByID");

            migrationBuilder.RenameColumn(
                name: "AccountOwnerId",
                table: "Accounts",
                newName: "AccountOwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_ModifiedById",
                table: "Accounts",
                newName: "IX_Accounts_ModifiedByID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_AccountOwnerId",
                table: "Accounts",
                newName: "IX_Accounts_AccountOwnerID");

            migrationBuilder.AlterColumn<int>(
                name: "StudentID",
                table: "VisaApplications",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "VisaApplications",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "VisaApplications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "InstituteIDAccountID",
                table: "VisaApplications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "VisaApplications",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedTime",
                table: "VisaApplications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ClientNumber",
                table: "Students",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByID",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "StudentID",
                table: "Enrollments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByID",
                table: "Enrollments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Enrollments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "InstituteID",
                table: "Enrollments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByID",
                table: "Enrollments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedTime",
                table: "Enrollments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedByID",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Accounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedTime",
                table: "Accounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_VisaApplications_CreatedById",
                table: "VisaApplications",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_VisaApplications_InstituteIDAccountID",
                table: "VisaApplications",
                column: "InstituteIDAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_VisaApplications_ModifiedById",
                table: "VisaApplications",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CreatedByID",
                table: "Students",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CreatedByID",
                table: "Enrollments",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_InstituteID",
                table: "Enrollments",
                column: "InstituteID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ModifiedByID",
                table: "Enrollments",
                column: "ModifiedByID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CreatedByID",
                table: "Accounts",
                column: "CreatedByID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_AccountOwnerID",
                table: "Accounts",
                column: "AccountOwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_CreatedByID",
                table: "Accounts",
                column: "CreatedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_ModifiedByID",
                table: "Accounts",
                column: "ModifiedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Students_AttendStudentCustomerID",
                table: "Activities",
                column: "AttendStudentCustomerID",
                principalTable: "Students",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_CreatedByID",
                table: "Enrollments",
                column: "CreatedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Accounts_EnrollmentAgentID",
                table: "Enrollments",
                column: "EnrollmentAgentID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Accounts_InstituteID",
                table: "Enrollments",
                column: "InstituteID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_ModifiedByID",
                table: "Enrollments",
                column: "ModifiedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentID",
                table: "Enrollments",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Accounts_AgentID",
                table: "Students",
                column: "AgentID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_CreatedByID",
                table: "Students",
                column: "CreatedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_CustomerOwnerID",
                table: "Students",
                column: "CustomerOwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_ModifiedByID",
                table: "Students",
                column: "ModifiedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaApplications_AspNetUsers_CreatedById",
                table: "VisaApplications",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaApplications_Accounts_InstituteIDAccountID",
                table: "VisaApplications",
                column: "InstituteIDAccountID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaApplications_AspNetUsers_ModifiedById",
                table: "VisaApplications",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaApplications_Students_StudentID",
                table: "VisaApplications",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_AccountOwnerID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_CreatedByID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_ModifiedByID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Students_AttendStudentCustomerID",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AspNetUsers_CreatedByID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Accounts_EnrollmentAgentID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Accounts_InstituteID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AspNetUsers_ModifiedByID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Accounts_AgentID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_CreatedByID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_CustomerOwnerID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_ModifiedByID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaApplications_AspNetUsers_CreatedById",
                table: "VisaApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaApplications_Accounts_InstituteIDAccountID",
                table: "VisaApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaApplications_AspNetUsers_ModifiedById",
                table: "VisaApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaApplications_Students_StudentID",
                table: "VisaApplications");

            migrationBuilder.DropIndex(
                name: "IX_VisaApplications_CreatedById",
                table: "VisaApplications");

            migrationBuilder.DropIndex(
                name: "IX_VisaApplications_InstituteIDAccountID",
                table: "VisaApplications");

            migrationBuilder.DropIndex(
                name: "IX_VisaApplications_ModifiedById",
                table: "VisaApplications");

            migrationBuilder.DropIndex(
                name: "IX_Students_CreatedByID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_CreatedByID",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_InstituteID",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_ModifiedByID",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CreatedByID",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "VisaApplications");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "VisaApplications");

            migrationBuilder.DropColumn(
                name: "InstituteIDAccountID",
                table: "VisaApplications");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "VisaApplications");

            migrationBuilder.DropColumn(
                name: "ModifiedTime",
                table: "VisaApplications");

            migrationBuilder.DropColumn(
                name: "ClientNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CreatedByID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CreatedByID",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "InstituteID",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "ModifiedByID",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "ModifiedTime",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CreatedByID",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ModifiedTime",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "ModifiedByID",
                table: "Students",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "ModifiedTime",
                table: "Students",
                newName: "RegisterDate");

            migrationBuilder.RenameColumn(
                name: "CustomerOwnerID",
                table: "Students",
                newName: "StudentOwnerId");

            migrationBuilder.RenameColumn(
                name: "AgentID",
                table: "Students",
                newName: "AgentAccountID");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Students",
                newName: "StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_Students_ModifiedByID",
                table: "Students",
                newName: "IX_Students_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Students_CustomerOwnerID",
                table: "Students",
                newName: "IX_Students_StudentOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_AgentID",
                table: "Students",
                newName: "IX_Students_AgentAccountID");

            migrationBuilder.RenameColumn(
                name: "EnrollmentAgentID",
                table: "Enrollments",
                newName: "InstituteAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_EnrollmentAgentID",
                table: "Enrollments",
                newName: "IX_Enrollments_InstituteAccountID");

            migrationBuilder.RenameColumn(
                name: "AttendStudentCustomerID",
                table: "Activities",
                newName: "AttendStudentStudentID");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_AttendStudentCustomerID",
                table: "Activities",
                newName: "IX_Activities_AttendStudentStudentID");

            migrationBuilder.RenameColumn(
                name: "ModifiedByID",
                table: "Accounts",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "AccountOwnerID",
                table: "Accounts",
                newName: "AccountOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_ModifiedByID",
                table: "Accounts",
                newName: "IX_Accounts_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_AccountOwnerID",
                table: "Accounts",
                newName: "IX_Accounts_AccountOwnerId");

            migrationBuilder.AlterColumn<int>(
                name: "StudentID",
                table: "VisaApplications",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "ClientNumber",
                table: "VisaApplications",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentID",
                table: "Enrollments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentAgentAccountID",
                table: "Enrollments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_EnrollmentAgentAccountID",
                table: "Enrollments",
                column: "EnrollmentAgentAccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_AccountOwnerId",
                table: "Accounts",
                column: "AccountOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_ModifiedById",
                table: "Accounts",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Students_AttendStudentStudentID",
                table: "Activities",
                column: "AttendStudentStudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Accounts_EnrollmentAgentAccountID",
                table: "Enrollments",
                column: "EnrollmentAgentAccountID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Accounts_InstituteAccountID",
                table: "Enrollments",
                column: "InstituteAccountID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentID",
                table: "Enrollments",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Accounts_AgentAccountID",
                table: "Students",
                column: "AgentAccountID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_ModifiedById",
                table: "Students",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_StudentOwnerId",
                table: "Students",
                column: "StudentOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaApplications_Students_StudentID",
                table: "VisaApplications",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
