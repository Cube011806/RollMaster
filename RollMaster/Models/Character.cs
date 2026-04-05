namespace RollMaster.Models
{
    public class Character
    {
        public int Id { get; set; }

        //Informacje podstawowe
        public string Imie { get; set; } = String.Empty;
        public string RodzimaKultura { get; set; } = String.Empty;
        public string RodzimaKorzysc { get; set; } = String.Empty;
        public string Powolanie { get; set; } = String.Empty;
        public int Wiek { get; set; }
        public string PoziomZycia { get; set; } = String.Empty;
        public string? Patron { get; set; }
        public string SciezkaCienia { get; set; } = String.Empty;
        public int Skarb { get; set; }
        public string? Wyrozniki { get; set; } = String.Empty;
        public string? Przywary { get; set; }

        //Sekcja Siła
        public int? WartoscSila { get; set; }
        public int? PTSila { get; set; }
        public int? Wytrzymalosc { get; set; }

        //Sekcja Serce
        public int? WartoscSerce { get; set; }
        public int? PTSerce { get; set; }
        public int? Nadzieja { get; set; }

        //Sekcja Rozum
        public int? WartoscRozum { get; set; }
        public int? PTRozum { get; set; }
        public int? Obrona { get; set; }

        //Relacja z umiejętnościami
        public IList<Skill> skills { get; set; } = new List<Skill>();

        //Nagrody
        public int? Mestwo { get; set; } = 0;
        public string? Nagrody { get; set; } = String.Empty;

        //Przymioty
        public int? Madrosc { get; set; } = 0;
        public string? Przymioty { get; set; } = String.Empty;

        //Relacja z broniami
        public List<Weapon> weapons { get; set; } = new List<Weapon>();
        public BodyArmor? Zbroja { get; set; }
        public Helmet? Helm { get; set; }
        public Shield? Tarcza { get; set; }
        //Rozwój
        public int? PunktyPrzygody { get; set; } = 0;
        public int? PunktyUmiejetnosci { get; set; } = 0;
        public int? PunktyZazylosci { get; set; } = 0;

        //Obecna wytrzymałość
        public int? ObecnaWytrzymalosc { get; set; } = 0;
        public int? Obciazenie { get; set; } = 0;
        public int? Znuzenie { get; set; } = 0;

        //Obecna nadzieja
        public int? ObecnaNadzieja { get; set; } = 0;
        public int? Cien { get; set; } = 0;
        public int? PietnoCienia { get; set; } = 0;

        //Ograniczenia
        public bool Wyczerpanie { get; set; } = false;
        public bool Przygnebienie { get; set; } = false;
        public bool Rana { get; set; } = false;
        public string? StopienRany { get; set; }

        //Ekwipunek
        public string? Ekwipunek { get; set; } = String.Empty;

        //Dodatkowe
        public string? Notatki { get; set; }

        /*----------------
        -----RELACJE-----
        ----------------*/
        public User User { get; set; }
        public string? UserId { get; set; }

        public Game? Game { get; set; }
        public int? GameId { get; set; }

    }
}
