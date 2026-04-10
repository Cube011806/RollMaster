using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RollMaster.Migrations
{
    /// <inheritdoc />
    public partial class v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Games_GameId",
                table: "Characters");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Games_GameId",
                table: "Characters",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Games_GameId",
                table: "Characters");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Games_GameId",
                table: "Characters",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }
    }
}
