using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderService.DataProvider.Migrations
{
    public partial class Changedrequestmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ExecutorRequests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "ExecutorRequests",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "CustomerRequests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "CustomerRequests",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ExecutorRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ExecutorRequests");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "CustomerRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CustomerRequests");
        }
    }
}
