using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.WebUI.Data
{
    public partial class correctVisaType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VisaAppliedType",
                table: "VisaApplications",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VisaAppliedType",
                table: "VisaApplications",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 50);
        }
    }
}
