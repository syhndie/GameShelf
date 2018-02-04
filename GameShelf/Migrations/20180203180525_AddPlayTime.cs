using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GameShelf.Migrations
{
    public partial class AddPlayTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayTimeID",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Playtimes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlayTimeCategory = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playtimes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GamePlayTimeLookups",
                columns: table => new
                {
                    GameID = table.Column<int>(nullable: false),
                    PlaytimeID = table.Column<int>(nullable: false)
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
                        name: "FK_GamePlayTimeLookups_Playtimes_PlaytimeID",
                        column: x => x.PlaytimeID,
                        principalTable: "Playtimes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayTimeID",
                table: "Games",
                column: "PlayTimeID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayTimeLookups_PlaytimeID",
                table: "GamePlayTimeLookups",
                column: "PlaytimeID");

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

            migrationBuilder.DropTable(
                name: "GamePlayTimeLookups");

            migrationBuilder.DropTable(
                name: "Playtimes");

            migrationBuilder.DropIndex(
                name: "IX_Games_PlayTimeID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PlayTimeID",
                table: "Games");
        }
    }
}
