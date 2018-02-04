using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GameShelf.Migrations
{
    public partial class FixingGameToPlayTimeRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayTimeLookups_Playtimes_PlaytimeID",
                table: "GamePlayTimeLookups");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Playtimes_PlayTimeID",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_PlayTimeID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PlayTimeID",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "PlaytimeID",
                table: "GamePlayTimeLookups",
                newName: "PlayTimeID");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlayTimeLookups_PlaytimeID",
                table: "GamePlayTimeLookups",
                newName: "IX_GamePlayTimeLookups_PlayTimeID");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayTimeLookups_Playtimes_PlayTimeID",
                table: "GamePlayTimeLookups",
                column: "PlayTimeID",
                principalTable: "Playtimes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayTimeLookups_Playtimes_PlayTimeID",
                table: "GamePlayTimeLookups");

            migrationBuilder.RenameColumn(
                name: "PlayTimeID",
                table: "GamePlayTimeLookups",
                newName: "PlaytimeID");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlayTimeLookups_PlayTimeID",
                table: "GamePlayTimeLookups",
                newName: "IX_GamePlayTimeLookups_PlaytimeID");

            migrationBuilder.AddColumn<int>(
                name: "PlayTimeID",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayTimeID",
                table: "Games",
                column: "PlayTimeID");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayTimeLookups_Playtimes_PlaytimeID",
                table: "GamePlayTimeLookups",
                column: "PlaytimeID",
                principalTable: "Playtimes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

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
