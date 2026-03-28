using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RollMaster.Data.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RodzimaKultura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Powolanie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wiek = table.Column<int>(type: "int", nullable: false),
                    PoziomZycia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patron = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SciezkaCienia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skarb = table.Column<int>(type: "int", nullable: false),
                    Wyrozniki = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Przywary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WartoscSila = table.Column<int>(type: "int", nullable: false),
                    PTSila = table.Column<int>(type: "int", nullable: false),
                    Wytrzymalosc = table.Column<int>(type: "int", nullable: false),
                    WartoscSerce = table.Column<int>(type: "int", nullable: false),
                    PTSerce = table.Column<int>(type: "int", nullable: false),
                    Nadzieja = table.Column<int>(type: "int", nullable: false),
                    WartoscRozum = table.Column<int>(type: "int", nullable: false),
                    PTRozum = table.Column<int>(type: "int", nullable: false),
                    Obrona = table.Column<int>(type: "int", nullable: false),
                    Czujnosc = table.Column<int>(type: "int", nullable: false),
                    Piesni = table.Column<int>(type: "int", nullable: false),
                    Polowanie = table.Column<int>(type: "int", nullable: false),
                    Respekt = table.Column<int>(type: "int", nullable: false),
                    Rzemioslo = table.Column<int>(type: "int", nullable: false),
                    Zwinnosc = table.Column<int>(type: "int", nullable: false),
                    WCzujnosc = table.Column<bool>(type: "bit", nullable: false),
                    WPiesni = table.Column<bool>(type: "bit", nullable: false),
                    WPolowanie = table.Column<bool>(type: "bit", nullable: false),
                    WRespekt = table.Column<bool>(type: "bit", nullable: false),
                    WRzemioslo = table.Column<bool>(type: "bit", nullable: false),
                    WZwinnosc = table.Column<bool>(type: "bit", nullable: false),
                    Przenikliwosc = table.Column<int>(type: "int", nullable: false),
                    Uprzejmosc = table.Column<int>(type: "int", nullable: false),
                    Uzdrawianie = table.Column<int>(type: "int", nullable: false),
                    Inspiracja = table.Column<int>(type: "int", nullable: false),
                    Wojaczka = table.Column<int>(type: "int", nullable: false),
                    Wedrowka = table.Column<int>(type: "int", nullable: false),
                    WPrzenikliwosc = table.Column<bool>(type: "bit", nullable: false),
                    WUprzejmosc = table.Column<bool>(type: "bit", nullable: false),
                    WUzdrawianie = table.Column<bool>(type: "bit", nullable: false),
                    WInspiracja = table.Column<bool>(type: "bit", nullable: false),
                    WWojaczka = table.Column<bool>(type: "bit", nullable: false),
                    WWedrowka = table.Column<bool>(type: "bit", nullable: false),
                    Szukanie = table.Column<int>(type: "int", nullable: false),
                    Zagadki = table.Column<int>(type: "int", nullable: false),
                    Rekonesans = table.Column<int>(type: "int", nullable: false),
                    Przekonywanie = table.Column<int>(type: "int", nullable: false),
                    Wiedza = table.Column<int>(type: "int", nullable: false),
                    Skradanie = table.Column<int>(type: "int", nullable: false),
                    WSzukanie = table.Column<bool>(type: "bit", nullable: false),
                    WZagadki = table.Column<bool>(type: "bit", nullable: false),
                    WRekonesans = table.Column<bool>(type: "bit", nullable: false),
                    WPrzekonywanie = table.Column<bool>(type: "bit", nullable: false),
                    WWiedza = table.Column<bool>(type: "bit", nullable: false),
                    WSkradanie = table.Column<bool>(type: "bit", nullable: false),
                    BBLuki = table.Column<int>(type: "int", nullable: false),
                    BBMiecze = table.Column<int>(type: "int", nullable: false),
                    BBTopory = table.Column<int>(type: "int", nullable: false),
                    BBWlocznie = table.Column<int>(type: "int", nullable: false),
                    Mestwo = table.Column<int>(type: "int", nullable: false),
                    Nagrody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Madrosc = table.Column<int>(type: "int", nullable: false),
                    Przymioty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    R1Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    R1Obrazenia = table.Column<int>(type: "int", nullable: false),
                    R1Przebicie = table.Column<int>(type: "int", nullable: false),
                    R1Obciazenie = table.Column<int>(type: "int", nullable: false),
                    R1Uwagi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    R2Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    R2Obrazenia = table.Column<int>(type: "int", nullable: false),
                    R2Przebicie = table.Column<int>(type: "int", nullable: false),
                    R2Obciazenie = table.Column<int>(type: "int", nullable: false),
                    R2Uwagi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    R3Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    R3Obrazenia = table.Column<int>(type: "int", nullable: false),
                    R3Przebicie = table.Column<int>(type: "int", nullable: false),
                    R3Obciazenie = table.Column<int>(type: "int", nullable: false),
                    R3Uwagi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    R4Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    R4Obrazenia = table.Column<int>(type: "int", nullable: false),
                    R4Przebicie = table.Column<int>(type: "int", nullable: false),
                    R4Obciazenie = table.Column<int>(type: "int", nullable: false),
                    R4Uwagi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZbrojaNazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZbrojaPancerz = table.Column<int>(type: "int", nullable: false),
                    ZbrojaObciazenie = table.Column<int>(type: "int", nullable: false),
                    HelmNazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HelmPancerz = table.Column<int>(type: "int", nullable: false),
                    HelmObciazenie = table.Column<int>(type: "int", nullable: false),
                    TarczaNazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TarczaObrona = table.Column<int>(type: "int", nullable: false),
                    TarczaObciazenie = table.Column<int>(type: "int", nullable: false),
                    PunktyPrzygody = table.Column<int>(type: "int", nullable: false),
                    PunktyUmiejetnosci = table.Column<int>(type: "int", nullable: false),
                    PunktyZazylosci = table.Column<int>(type: "int", nullable: false),
                    ObecnaWytrzymalosc = table.Column<int>(type: "int", nullable: false),
                    Obciazenie = table.Column<int>(type: "int", nullable: false),
                    Znuzenie = table.Column<int>(type: "int", nullable: false),
                    ObecnaNadzieja = table.Column<int>(type: "int", nullable: false),
                    Cien = table.Column<int>(type: "int", nullable: false),
                    PietnoCienia = table.Column<int>(type: "int", nullable: false),
                    Wyczerpanie = table.Column<bool>(type: "bit", nullable: false),
                    Przygnebienie = table.Column<bool>(type: "bit", nullable: false),
                    Rana = table.Column<bool>(type: "bit", nullable: false),
                    StopienRany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ekwipunek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SzczegolnaSympatia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Character_UserId",
                table: "Character",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
