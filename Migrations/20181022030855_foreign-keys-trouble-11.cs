using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mementoscraperapi.Migrations
{
    public partial class foreignkeystrouble11 : Migration
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
                    PHRASE = table.Column<string>(nullable: false),
                    CREATION = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MEMENTO_PK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "MementoScraperDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Memories",
                schema: "MementoScraperDatabase",
                columns: table => new
                {
                    MemoryId = table.Column<long>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MementoForeignKey = table.Column<int>(nullable: false),
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
                        name: "FK_Memories_Mementos_MementoForeignKey",
                        column: x => x.MementoForeignKey,
                        principalSchema: "MementoScraperDatabase",
                        principalTable: "Mementos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CronDetails",
                schema: "MementoScraperDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Frequency = table.Column<string>(nullable: true),
                    Hashtag = table.Column<string>(nullable: true),
                    Facebook = table.Column<bool>(nullable: false),
                    Twitter = table.Column<bool>(nullable: false),
                    Instagram = table.Column<bool>(nullable: false),
                    Modification = table.Column<DateTime>(nullable: false),
                    Creation = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CronDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CronDetails_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "MementoScraperDatabase",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CronDetails_UserId",
                schema: "MementoScraperDatabase",
                table: "CronDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Memories_MementoForeignKey",
                schema: "MementoScraperDatabase",
                table: "Memories",
                column: "MementoForeignKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CronDetails",
                schema: "MementoScraperDatabase");

            migrationBuilder.DropTable(
                name: "Memories",
                schema: "MementoScraperDatabase");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "MementoScraperDatabase");

            migrationBuilder.DropTable(
                name: "Mementos",
                schema: "MementoScraperDatabase");
        }
    }
}
