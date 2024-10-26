using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kwikker_Backend.Migrations
{
    /// <inheritdoc />
    public partial class addDecayIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Trends_DecayScore",
                table: "Trends",
                column: "DecayScore");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trends_DecayScore",
                table: "Trends");
        }
    }
}
