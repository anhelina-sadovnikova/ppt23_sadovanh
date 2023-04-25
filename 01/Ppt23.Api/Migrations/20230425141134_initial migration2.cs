using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ppt23.Api.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Vybavenis",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "dateBuy",
                table: "Vybavenis",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "lastRev",
                table: "Vybavenis",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Vybavenis");

            migrationBuilder.DropColumn(
                name: "dateBuy",
                table: "Vybavenis");

            migrationBuilder.DropColumn(
                name: "lastRev",
                table: "Vybavenis");
        }
    }
}
