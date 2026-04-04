using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RollMaster.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BBLuki",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BBMiecze",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BBTopory",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BBWlocznie",
                table: "Characters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BBLuki",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BBMiecze",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BBTopory",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BBWlocznie",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
