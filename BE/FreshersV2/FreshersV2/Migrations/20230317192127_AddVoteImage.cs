using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreshersV2.Migrations
{
    public partial class AddVoteImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoteImageContestId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false),
                    VoteTime = table.Column<int>(type: "int", nullable: false),
                    DrawTime = table.Column<int>(type: "int", nullable: false),
                    Words = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoundNumber = table.Column<int>(type: "int", nullable: false),
                    ContestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rounds_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserContests",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContestId = table.Column<int>(type: "int", nullable: false),
                    UserHubId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VoteImageContestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContests", x => new { x.UserId, x.ContestId });
                    table.ForeignKey(
                        name: "FK_UserContests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserContests_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserContests_Contests_VoteImageContestId",
                        column: x => x.VoteImageContestId,
                        principalTable: "Contests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VoteImageRound",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoundId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteImageRound", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteImageRound_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoundDrawingUsers",
                columns: table => new
                {
                    RoundId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundDrawingUsers", x => new { x.UserId, x.RoundId });
                    table.ForeignKey(
                        name: "FK_RoundDrawingUsers_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoundDrawingUsers_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoundDrawingUsers_UserContests_UserId_ContestId",
                        columns: x => new { x.UserId, x.ContestId },
                        principalTable: "UserContests",
                        principalColumns: new[] { "UserId", "ContestId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoundVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoundId = table.Column<int>(type: "int", nullable: false),
                    VoteImageRoundId = table.Column<int>(type: "int", nullable: false),
                    Image1Votes = table.Column<int>(type: "int", nullable: false),
                    Image2Votes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoundVotes_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoundVotes_VoteImageRound_VoteImageRoundId",
                        column: x => x.VoteImageRoundId,
                        principalTable: "VoteImageRound",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VoteImageRoundId = table.Column<int>(type: "int", nullable: false),
                    ContestId = table.Column<int>(type: "int", nullable: false),
                    ContestId1 = table.Column<int>(type: "int", nullable: false),
                    RoundVoteId = table.Column<int>(type: "int", nullable: false),
                    Base64Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoundId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Images_Contests_ContestId1",
                        column: x => x.ContestId1,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Images_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_RoundVotes_RoundVoteId",
                        column: x => x.RoundVoteId,
                        principalTable: "RoundVotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Images_UserContests_UserId_ContestId",
                        columns: x => new { x.UserId, x.ContestId },
                        principalTable: "UserContests",
                        principalColumns: new[] { "UserId", "ContestId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Images_VoteImageRound_VoteImageRoundId",
                        column: x => x.VoteImageRoundId,
                        principalTable: "VoteImageRound",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VoteImageContestId",
                table: "AspNetUsers",
                column: "VoteImageContestId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ContestId",
                table: "Images",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ContestId1",
                table: "Images",
                column: "ContestId1");

            migrationBuilder.CreateIndex(
                name: "IX_Images_RoundId",
                table: "Images",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_RoundVoteId",
                table: "Images",
                column: "RoundVoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserId_ContestId",
                table: "Images",
                columns: new[] { "UserId", "ContestId" });

            migrationBuilder.CreateIndex(
                name: "IX_Images_VoteImageRoundId",
                table: "Images",
                column: "VoteImageRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundDrawingUsers_ContestId",
                table: "RoundDrawingUsers",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundDrawingUsers_RoundId",
                table: "RoundDrawingUsers",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundDrawingUsers_UserId_ContestId",
                table: "RoundDrawingUsers",
                columns: new[] { "UserId", "ContestId" });

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_ContestId",
                table: "Rounds",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundVotes_RoundId",
                table: "RoundVotes",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundVotes_VoteImageRoundId",
                table: "RoundVotes",
                column: "VoteImageRoundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserContests_ContestId",
                table: "UserContests",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserContests_VoteImageContestId",
                table: "UserContests",
                column: "VoteImageContestId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteImageRound_RoundId",
                table: "VoteImageRound",
                column: "RoundId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Contests_VoteImageContestId",
                table: "AspNetUsers",
                column: "VoteImageContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Contests_VoteImageContestId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "RoundDrawingUsers");

            migrationBuilder.DropTable(
                name: "RoundVotes");

            migrationBuilder.DropTable(
                name: "UserContests");

            migrationBuilder.DropTable(
                name: "VoteImageRound");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "Contests");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_VoteImageContestId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VoteImageContestId",
                table: "AspNetUsers");
        }
    }
}
