using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RollMaster.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HelmNazwa",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "HelmObciazenie",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "HelmPancerz",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "TarczaNazwa",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "TarczaObciazenie",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "TarczaObrona",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "ZbrojaNazwa",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "ZbrojaObciazenie",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "ZbrojaPancerz",
                table: "Characters");

            migrationBuilder.CreateTable(
                name: "Armors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Obciazenie = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Pancerz = table.Column<int>(type: "int", nullable: true),
                    Helmet_Pancerz = table.Column<int>(type: "int", nullable: true),
                    Obrona = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Armors_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Armors_CharacterId",
                table: "Armors",
                column: "CharacterId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Armors");

            migrationBuilder.AddColumn<string>(
                name: "HelmNazwa",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HelmObciazenie",
                table: "Characters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HelmPancerz",
                table: "Characters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TarczaNazwa",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TarczaObciazenie",
                table: "Characters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TarczaObrona",
                table: "Characters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZbrojaNazwa",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZbrojaObciazenie",
                table: "Characters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZbrojaPancerz",
                table: "Characters",
                type: "int",
                nullable: true);
        }
    }
}
