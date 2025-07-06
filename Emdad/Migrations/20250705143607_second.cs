using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emdad.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CitizensSettings",
                columns: table => new
                {
                    CitizensSettingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenNationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CitizenFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenAbout = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizensSettings", x => x.CitizensSettingsId);
                    table.ForeignKey(
                        name: "FK_CitizensSettings_Citizen_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizen",
                        principalColumn: "CitizenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CitizensSettings_CitizenId",
                table: "CitizensSettings",
                column: "CitizenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CitizensSettings");
        }
    }
}
