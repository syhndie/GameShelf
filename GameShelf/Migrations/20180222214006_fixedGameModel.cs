using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GameShelf.Migrations
{
    public partial class fixedGameModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Playtimes_PlayTimeID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PublicationYear",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "PlayTimeID",
                table: "Games",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Playtimes_PlayTimeID",
                table: "Games",
                column: "PlayTimeID",
                principalTable: "Playtimes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Playtimes_PlayTimeID",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "PlayTimeID",
                table: "Games",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PublicationYear",
                table: "Games",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Playtimes_PlayTimeID",
                table: "Games",
                column: "PlayTimeID",
                principalTable: "Playtimes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
