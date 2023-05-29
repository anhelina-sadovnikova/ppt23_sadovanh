using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ppt23.Api.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrationaddrevize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastRev",
                table: "Vybavenis");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Revisions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "VybaveniId",
                table: "Revisions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_VybaveniId",
                table: "Revisions",
                column: "VybaveniId");

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_Vybavenis_VybaveniId",
                table: "Revisions",
                column: "VybaveniId",
                principalTable: "Vybavenis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_Vybavenis_VybaveniId",
                table: "Revisions");

            migrationBuilder.DropIndex(
                name: "IX_Revisions_VybaveniId",
                table: "Revisions");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Revisions");

            migrationBuilder.DropColumn(
                name: "VybaveniId",
                table: "Revisions");

            migrationBuilder.AddColumn<DateTime>(
                name: "lastRev",
                table: "Vybavenis",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
