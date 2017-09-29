using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Myapi.Migrations
{
    public partial class Application : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false),
                    AccountID = table.Column<Guid>(type: "char(36)", nullable: false),
                    AppId = table.Column<Guid>(type: "char(36)", nullable: false),
                    AppName = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    AppSecret = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");
        }
    }
}
