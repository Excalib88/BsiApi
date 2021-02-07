using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsiMobile.Web.Migrations
{
    public partial class AesFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Iv",
                table: "Users",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Key",
                table: "Users",
                type: "bytea",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iv",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Users");
        }
    }
}
