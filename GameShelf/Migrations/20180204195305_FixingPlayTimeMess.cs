using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GameShelf.Migrations
{
    public partial class FixingPlayTimeMess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlayTimeLookups");

            migrationBuilder.AddColumn<int>(
                name: "PlayTimeID",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayTimeID",
                table: "Games",
                column: "PlayTimeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Playtimes_PlayTimeID",
                table: "Games",
                column: "PlayTimeID",
                principalTable: "Playtimes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Playtimes_PlayTimeID",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_PlayTimeID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PlayTimeID",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GamePlayTimeLookups",
                columns: table => new
                {
                    GameID = table.Column<int>(nullable: false),
                    PlayTimeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayTimeLookups", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_GamePlayTimeLookups_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlayTimeLookups_Playtimes_PlayTimeID",
                        column: x => x.PlayTimeID,
                        principalTable: "Playtimes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayTimeLookups_PlayTimeID",
                table: "GamePlayTimeLookups",
                column: "PlayTimeID");
        }
    }
}
