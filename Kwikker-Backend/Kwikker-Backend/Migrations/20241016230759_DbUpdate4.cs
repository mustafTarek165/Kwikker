using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kwikker_Backend.Migrations
{
    /// <inheritdoc />
    public partial class DbUpdate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timelines");

            migrationBuilder.CreateTable(
                name: "Trends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hashtag = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Occurrences = table.Column<int>(type: "int", nullable: false),
                    LastOccurred = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DecayScore = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TweetTrends",
                columns: table => new
                {
                    TweetId = table.Column<int>(type: "int", nullable: false),
                    TrendId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetTrends", x => new { x.TweetId, x.TrendId });
                    table.ForeignKey(
                        name: "FK_TweetTrends_Trends_TrendId",
                        column: x => x.TrendId,
                        principalTable: "Trends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TweetTrends_Tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "Tweets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trends_Hashtag",
                table: "Trends",
                column: "Hashtag",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TweetTrends_TrendId_TweetId",
                table: "TweetTrends",
                columns: new[] { "TrendId", "TweetId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TweetTrends");

            migrationBuilder.DropTable(
                name: "Trends");

            migrationBuilder.CreateTable(
                name: "Timelines",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TweetId = table.Column<int>(type: "int", nullable: false),
                    TimelineType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timelines", x => new { x.UserId, x.TweetId, x.TimelineType });
                    table.ForeignKey(
                        name: "FK_Timelines_Tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "Tweets",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Timelines_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Timelines_TweetId",
                table: "Timelines",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_Timelines_UserId_TimelineType",
                table: "Timelines",
                columns: new[] { "UserId", "TimelineType" });
        }
    }
}
