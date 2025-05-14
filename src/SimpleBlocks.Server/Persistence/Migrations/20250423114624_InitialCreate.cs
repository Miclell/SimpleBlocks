using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBlocks.Server.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LanguageFileSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageKey = table.Column<string>(type: "text", nullable: false),
                    BlocksJson = table.Column<string>(type: "text", nullable: false),
                    SemanticsJson = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageFileSets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanguageFileSets_LanguageKey",
                table: "LanguageFileSets",
                column: "LanguageKey",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanguageFileSets");
        }
    }
}
