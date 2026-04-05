using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RollMaster.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Armors_Characters_CharacterId",
                table: "Armors");

            migrationBuilder.DropIndex(
                name: "IX_Armors_CharacterId",
                table: "Armors");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Armors");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Armors");

            migrationBuilder.DropColumn(
                name: "Helmet_Pancerz",
                table: "Armors");

            migrationBuilder.DropColumn(
                name: "Obrona",
                table: "Armors");

            migrationBuilder.DropColumn(
                name: "Pancerz",
                table: "Armors");

            migrationBuilder.CreateTable(
                name: "BodyArmors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Pancerz = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyArmors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyArmors_Armors_Id",
                        column: x => x.Id,
                        principalTable: "Armors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BodyArmors_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Helmets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Pancerz = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Helmets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Helmets_Armors_Id",
                        column: x => x.Id,
                        principalTable: "Armors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Helmets_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Obrona = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shields_Armors_Id",
                        column: x => x.Id,
                        principalTable: "Armors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shields_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BodyArmors_CharacterId",
                table: "BodyArmors",
                column: "CharacterId",
                unique: true,
                filter: "[CharacterId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Helmets_CharacterId",
                table: "Helmets",
                column: "CharacterId",
                unique: true,
                filter: "[CharacterId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Shields_CharacterId",
                table: "Shields",
                column: "CharacterId",
                unique: true,
                filter: "[CharacterId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyArmors");

            migrationBuilder.DropTable(
                name: "Helmets");

            migrationBuilder.DropTable(
                name: "Shields");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Armors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Armors",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Helmet_Pancerz",
                table: "Armors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Obrona",
                table: "Armors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pancerz",
                table: "Armors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Armors_CharacterId",
                table: "Armors",
                column: "CharacterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Armors_Characters_CharacterId",
                table: "Armors",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
