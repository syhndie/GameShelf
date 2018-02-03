using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GameShelf.Migrations
{
    public partial class AddedRoleBackToPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Relationships",
                table: "Relationships");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relationships",
                table: "Relationships",
                columns: new[] { "GameID", "PersonID", "Role" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Relationships",
                table: "Relationships");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relationships",
                table: "Relationships",
                columns: new[] { "GameID", "PersonID" });
        }
    }
}
