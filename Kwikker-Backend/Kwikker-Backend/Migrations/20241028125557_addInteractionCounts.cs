using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kwikker_Backend.Migrations
{
    /// <inheritdoc />
    public partial class addInteractionCounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookmarksNumber",
                table: "Tweets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LikesNumber",
                table: "Tweets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RetweetsNumber",
                table: "Tweets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookmarksNumber",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "LikesNumber",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "RetweetsNumber",
                table: "Tweets");
        }
    }
}
