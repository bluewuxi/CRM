using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.WebUI.Data
{
    public partial class checkpoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Accounts_AttendedAccountID",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Customers_AttendedCustomerID",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaApplications_Accounts_InstituteAccountID",
                table: "VisaApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaApplications_Accounts_InstituteIDAccountID",
                table: "VisaApplications");

            migrationBuilder.DropIndex(
                name: "IX_VisaApplications_InstituteAccountID",
                table: "VisaApplications");

            migrationBuilder.DropIndex(
                name: "IX_VisaApplications_InstituteIDAccountID",
                table: "VisaApplications");

            migrationBuilder.DropColumn(
                name: "InstituteAccountID",
                table: "VisaApplications");

            migrationBuilder.DropColumn(
                name: "InstituteIDAccountID",
                table: "VisaApplications");

            migrationBuilder.AddColumn<int>(
                name: "InstituteID",
                table: "VisaApplications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AttendedCustomerID",
                table: "Activities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AttendedAccountID",
                table: "Activities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_VisaApplications_InstituteID",
                table: "VisaApplications",
                column: "InstituteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Accounts_AttendedAccountID",
                table: "Activities",
                column: "AttendedAccountID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Customers_AttendedCustomerID",
                table: "Activities",
                column: "AttendedCustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaApplications_Accounts_InstituteID",
                table: "VisaApplications",
                column: "InstituteID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Accounts_AttendedAccountID",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Customers_AttendedCustomerID",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaApplications_Accounts_InstituteID",
                table: "VisaApplications");

            migrationBuilder.DropIndex(
                name: "IX_VisaApplications_InstituteID",
                table: "VisaApplications");

            migrationBuilder.DropColumn(
                name: "InstituteID",
                table: "VisaApplications");

            migrationBuilder.AddColumn<int>(
                name: "InstituteAccountID",
                table: "VisaApplications",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstituteIDAccountID",
                table: "VisaApplications",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttendedCustomerID",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttendedAccountID",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisaApplications_InstituteAccountID",
                table: "VisaApplications",
                column: "InstituteAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_VisaApplications_InstituteIDAccountID",
                table: "VisaApplications",
                column: "InstituteIDAccountID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_VisaApplications_Accounts_InstituteAccountID",
                table: "VisaApplications",
                column: "InstituteAccountID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaApplications_Accounts_InstituteIDAccountID",
                table: "VisaApplications",
                column: "InstituteIDAccountID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
