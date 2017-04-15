using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.WebUI.Data
{
    public partial class activiy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Accounts_AttendAccountAccountID",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Customers_AttendStudentCustomerID1",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Customers_CustomerID",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_AttendAccountAccountID",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_AttendStudentCustomerID1",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_CustomerID",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "AttendAccountAccountID",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "AttendStudentCustomerID1",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "ActivityTime",
                table: "Activities",
                newName: "StartTime");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Activities",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "ActivityOwnerID",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActivityType",
                table: "Activities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttendedAccountID",
                table: "Activities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttendedCustomerID",
                table: "Activities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Activities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActivityOwnerID",
                table: "Activities",
                column: "ActivityOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AttendedAccountID",
                table: "Activities",
                column: "AttendedAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AttendedCustomerID",
                table: "Activities",
                column: "AttendedCustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_AspNetUsers_ActivityOwnerID",
                table: "Activities",
                column: "ActivityOwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Accounts_AttendedAccountID",
                table: "Activities",
                column: "AttendedAccountID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Customers_AttendedCustomerID",
                table: "Activities",
                column: "AttendedCustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_AspNetUsers_ActivityOwnerID",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Accounts_AttendedAccountID",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Customers_AttendedCustomerID",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_ActivityOwnerID",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_AttendedAccountID",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_AttendedCustomerID",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ActivityOwnerID",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ActivityType",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "AttendedAccountID",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "AttendedCustomerID",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Activities",
                newName: "ActivityTime");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttendAccountAccountID",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttendStudentCustomerID1",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Activities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AttendAccountAccountID",
                table: "Activities",
                column: "AttendAccountAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AttendStudentCustomerID1",
                table: "Activities",
                column: "AttendStudentCustomerID1");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CustomerID",
                table: "Activities",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Accounts_AttendAccountAccountID",
                table: "Activities",
                column: "AttendAccountAccountID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Customers_AttendStudentCustomerID1",
                table: "Activities",
                column: "AttendStudentCustomerID1",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Customers_CustomerID",
                table: "Activities",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
