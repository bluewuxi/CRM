using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRM.WebUI.Data
{
    public partial class full2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Students_AttendStudentCustomerID",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_Accounts_ApplicationAgentID",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_AspNetUsers_CreatedByID",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_Accounts_InstituteID",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_AspNetUsers_ModifiedByID",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_Students_StudentID",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaApplications_Students_StudentID",
                table: "VisaApplications");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Application",
                table: "Application");

            migrationBuilder.RenameTable(
                name: "Application",
                newName: "Applications");

            migrationBuilder.RenameIndex(
                name: "IX_Application_StudentID",
                table: "Applications",
                newName: "IX_Applications_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_Application_ModifiedByID",
                table: "Applications",
                newName: "IX_Applications_ModifiedByID");

            migrationBuilder.RenameIndex(
                name: "IX_Application_InstituteID",
                table: "Applications",
                newName: "IX_Applications_InstituteID");

            migrationBuilder.RenameIndex(
                name: "IX_Application_CreatedByID",
                table: "Applications",
                newName: "IX_Applications_CreatedByID");

            migrationBuilder.RenameIndex(
                name: "IX_Application_ApplicationAgentID",
                table: "Applications",
                newName: "IX_Applications_ApplicationAgentID");

            migrationBuilder.RenameColumn(
                name: "AttendStudentCustomerID",
                table: "Activities",
                newName: "CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_AttendStudentCustomerID",
                table: "Activities",
                newName: "IX_Activities_CustomerID");

            migrationBuilder.AddColumn<int>(
                name: "AttendStudentCustomerID1",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                column: "ApplicationID");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcademicBackground = table.Column<string>(maxLength: 200, nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    CreatedByID = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CustomerOwnerID = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    EMail = table.Column<string>(maxLength: 50, nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Mobile = table.Column<string>(maxLength: 30, nullable: true),
                    ModifiedByID = table.Column<string>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Note = table.Column<string>(nullable: true),
                    PreferName = table.Column<string>(maxLength: 50, nullable: true),
                    Source = table.Column<string>(maxLength: 50, nullable: true),
                    AgentID = table.Column<int>(nullable: true),
                    ClientNumber = table.Column<string>(maxLength: 20, nullable: true),
                    ContactName = table.Column<string>(maxLength: 50, nullable: true),
                    Nationality = table.Column<string>(maxLength: 20, nullable: true),
                    PassportNumber = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_CustomerOwnerID",
                        column: x => x.CustomerOwnerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_ModifiedByID",
                        column: x => x.ModifiedByID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Accounts_AgentID",
                        column: x => x.AgentID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AttendStudentCustomerID1",
                table: "Activities",
                column: "AttendStudentCustomerID1");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedByID",
                table: "Customers",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerOwnerID",
                table: "Customers",
                column: "CustomerOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ModifiedByID",
                table: "Customers",
                column: "ModifiedByID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AgentID",
                table: "Customers",
                column: "AgentID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Accounts_ApplicationAgentID",
                table: "Applications",
                column: "ApplicationAgentID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_CreatedByID",
                table: "Applications",
                column: "CreatedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Accounts_InstituteID",
                table: "Applications",
                column: "InstituteID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_ModifiedByID",
                table: "Applications",
                column: "ModifiedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Customers_StudentID",
                table: "Applications",
                column: "StudentID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Customers_StudentID",
                table: "Enrollments",
                column: "StudentID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaApplications_Customers_StudentID",
                table: "VisaApplications",
                column: "StudentID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Customers_AttendStudentCustomerID1",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Customers_CustomerID",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Accounts_ApplicationAgentID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_CreatedByID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Accounts_InstituteID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_ModifiedByID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Customers_StudentID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Customers_StudentID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaApplications_Customers_StudentID",
                table: "VisaApplications");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Activities_AttendStudentCustomerID1",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "AttendStudentCustomerID1",
                table: "Activities");

            migrationBuilder.RenameTable(
                name: "Applications",
                newName: "Application");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_StudentID",
                table: "Application",
                newName: "IX_Application_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_ModifiedByID",
                table: "Application",
                newName: "IX_Application_ModifiedByID");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_InstituteID",
                table: "Application",
                newName: "IX_Application_InstituteID");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_CreatedByID",
                table: "Application",
                newName: "IX_Application_CreatedByID");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_ApplicationAgentID",
                table: "Application",
                newName: "IX_Application_ApplicationAgentID");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Activities",
                newName: "AttendStudentCustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_CustomerID",
                table: "Activities",
                newName: "IX_Activities_AttendStudentCustomerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Application",
                table: "Application",
                column: "ApplicationID");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcademicBackground = table.Column<string>(maxLength: 200, nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    AgentID = table.Column<int>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    ClientNumber = table.Column<string>(maxLength: 20, nullable: true),
                    ContactName = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedByID = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CustomerOwnerID = table.Column<string>(nullable: true),
                    EMail = table.Column<string>(maxLength: 50, nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Mobile = table.Column<string>(maxLength: 30, nullable: true),
                    ModifiedByID = table.Column<string>(nullable: true),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Nationality = table.Column<string>(maxLength: 20, nullable: true),
                    Note = table.Column<string>(nullable: true),
                    PassportNumber = table.Column<string>(maxLength: 20, nullable: true),
                    PreferName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_Students_Accounts_AgentID",
                        column: x => x.AgentID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_CustomerOwnerID",
                        column: x => x.CustomerOwnerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_ModifiedByID",
                        column: x => x.ModifiedByID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_AgentID",
                table: "Students",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CreatedByID",
                table: "Students",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CustomerOwnerID",
                table: "Students",
                column: "CustomerOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ModifiedByID",
                table: "Students",
                column: "ModifiedByID");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Students_AttendStudentCustomerID",
                table: "Activities",
                column: "AttendStudentCustomerID",
                principalTable: "Students",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Accounts_ApplicationAgentID",
                table: "Application",
                column: "ApplicationAgentID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_AspNetUsers_CreatedByID",
                table: "Application",
                column: "CreatedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Accounts_InstituteID",
                table: "Application",
                column: "InstituteID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_AspNetUsers_ModifiedByID",
                table: "Application",
                column: "ModifiedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Students_StudentID",
                table: "Application",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentID",
                table: "Enrollments",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaApplications_Students_StudentID",
                table: "VisaApplications",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
