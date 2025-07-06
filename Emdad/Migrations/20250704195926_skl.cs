using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emdad.Migrations
{
    /// <inheritdoc />
    public partial class skl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Citizen_CitizenId",
                table: "Submission");

            migrationBuilder.DropIndex(
                name: "IX_Submission_CitizenId",
                table: "Submission");

            migrationBuilder.DropColumn(
                name: "CitizenId",
                table: "Submission");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CitizenId",
                table: "Submission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Submission_CitizenId",
                table: "Submission",
                column: "CitizenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Citizen_CitizenId",
                table: "Submission",
                column: "CitizenId",
                principalTable: "Citizen",
                principalColumn: "CitizenId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
