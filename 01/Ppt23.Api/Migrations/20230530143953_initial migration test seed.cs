using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ppt23.Api.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrationtestseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ukons_Workers_PracovnikId",
                table: "Ukons");

            migrationBuilder.AlterColumn<Guid>(
                name: "PracovnikId",
                table: "Ukons",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "Ukons",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Ukons_Workers_PracovnikId",
                table: "Ukons",
                column: "PracovnikId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ukons_Workers_PracovnikId",
                table: "Ukons");

            migrationBuilder.DropColumn(
                name: "Detail",
                table: "Ukons");

            migrationBuilder.AlterColumn<Guid>(
                name: "PracovnikId",
                table: "Ukons",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Ukons_Workers_PracovnikId",
                table: "Ukons",
                column: "PracovnikId",
                principalTable: "Workers",
                principalColumn: "Id");
        }
    }
}
