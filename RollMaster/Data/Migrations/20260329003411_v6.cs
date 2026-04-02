using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RollMaster.Data.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Czujnosc",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Inspiracja",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Piesni",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Polowanie",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Przekonywanie",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Przenikliwosc",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Rekonesans",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Respekt",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Rzemioslo",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Skradanie",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Szukanie",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Uprzejmosc",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Uzdrawianie",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WCzujnosc",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WInspiracja",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WPiesni",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WPolowanie",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WPrzekonywanie",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WPrzenikliwosc",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WRekonesans",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WRespekt",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WRzemioslo",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WSkradanie",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WSzukanie",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WUprzejmosc",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WUzdrawianie",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WWedrowka",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WWiedza",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WWojaczka",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WZagadki",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WZwinnosc",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Wedrowka",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Wiedza",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Wojaczka",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Zagadki",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Zwinnosc",
                table: "Character");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Czujnosc",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Inspiracja",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Piesni",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Polowanie",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Przekonywanie",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Przenikliwosc",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rekonesans",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Respekt",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rzemioslo",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Skradanie",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Szukanie",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Uprzejmosc",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Uzdrawianie",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "WCzujnosc",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WInspiracja",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WPiesni",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WPolowanie",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WPrzekonywanie",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WPrzenikliwosc",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WRekonesans",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WRespekt",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WRzemioslo",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WSkradanie",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WSzukanie",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WUprzejmosc",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WUzdrawianie",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WWedrowka",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WWiedza",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WWojaczka",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WZagadki",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WZwinnosc",
                table: "Character",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Wedrowka",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wiedza",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wojaczka",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Zagadki",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Zwinnosc",
                table: "Character",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
