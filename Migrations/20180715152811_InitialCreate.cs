using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mementoscraperapi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MementoScraperDatabase");

            migrationBuilder.CreateTable(
                name: "Mementos",
                schema: "MementoScraperDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MementoId = table.Column<int>(nullable: false),
                    COMMENT = table.Column<string>(nullable: true),
                    OWNER = table.Column<string>(nullable: false),
                    TYPE = table.Column<string>(nullable: false),
                    CREATION = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MEMENTO_PK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Memories",
                schema: "MementoScraperDatabase",
                columns: table => new
                {
                    MemoryId = table.Column<long>(nullable: true),
                    Id = table.Column<int>(nullable: false),
                    MementoId = table.Column<int>(nullable: false),
                    MEDIA_URL = table.Column<string>(nullable: true),
                    MEDIA_URL_HTTPS = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    DISPLAY_URL = table.Column<string>(nullable: true),
                    EXPANDED_URL = table.Column<string>(nullable: true),
                    MEDIA_TYPE = table.Column<string>(nullable: false),
                    CREATION = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MEMORY_PK", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memories_Mementos_Id",
                        column: x => x.Id,
                        principalSchema: "MementoScraperDatabase",
                        principalTable: "Mementos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Memories",
                schema: "MementoScraperDatabase");

            migrationBuilder.DropTable(
                name: "Mementos",
                schema: "MementoScraperDatabase");
        }
    }
}
