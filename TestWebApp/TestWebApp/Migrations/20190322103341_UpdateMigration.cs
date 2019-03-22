using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWebApp.Migrations
{
    public partial class UpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Emails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FailedMessage",
                table: "Emails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "Emails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "FailedMessage",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "Emails");
        }
    }
}
