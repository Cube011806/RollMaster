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
        public int WartoscSila { get; set; }
        public int PTSila { get; set; }
        public int Wytrzymalosc { get; set; }

        //Sekcja Serce
        public int WartoscSerce { get; set; }
        public int PTSerce { get; set; }
        public int Nadzieja { get; set; }

        //Sekcja Rozum
        public int WartoscRozum { get; set; }
        public int PTRozum { get; set; }
        public int Obrona { get; set; }

        //Relacja z umiejętnościami
        public IList<Skill> skills { get; set; } = new List<Skill>();

        //Biegłosci Bojowe
        //public int BBLuki { get; set; } = 0;
        //public int BBMiecze { get; set; } = 0;
        //public int BBTopory { get; set; } = 0;
        //public int BBWlocznie { get; set; } = 0;

        //Nagrody
        public int Mestwo { get; set; } = 0;
        public string? Nagrody { get; set; } = String.Empty;

        //Przymioty
        public int Madrosc { get; set; } = 0;
        public string? Przymioty { get; set; } = String.Empty;

        //Relacja z broniami
        public List<Weapon> weapons { get; set; } = new List<Weapon>();

        //Zbroja
        public string? ZbrojaNazwa { get; set; }
        public int? ZbrojaPancerz { get; set; }
        public int? ZbrojaObciazenie { get; set; }
        //Hełm
        public string? HelmNazwa { get; set; }
        public int? HelmPancerz { get; set; }
        public int? HelmObciazenie { get; set; }
        //Tarcza
        public string? TarczaNazwa { get; set; }
        public int? TarczaObrona { get; set; }
        public int? TarczaObciazenie { get; set; }

        //Rozwój
        public int PunktyPrzygody { get; set; } = 0;
        public int PunktyUmiejetnosci { get; set; } = 0;
        public int PunktyZazylosci { get; set; } = 0;

        //Obecna wytrzymałość
        public int ObecnaWytrzymalosc { get; set; } = 0;
        public int Obciazenie { get; set; } = 0;
        public int Znuzenie { get; set; } = 0;

        //Obecna nadzieja
        public int ObecnaNadzieja { get; set; } = 0;
        public int Cien { get; set; } = 0;
        public int PietnoCienia { get; set; } = 0;

        //Ograniczenia
        public bool Wyczerpanie { get; set; } = false;
        public bool Przygnebienie { get; set; } = false;
        public bool Rana { get; set; } = false;
        public string? StopienRany { get; set; }

        //Ekwipunek
        public string? Ekwipunek { get; set; } = String.Empty;

        //Dodatkowe
        public string? SzczegolnaSympatia { get; set; }

        /*----------------
        -----RELACJE-----
        ----------------*/
        public User User { get; set; }
        public string? UserId { get; set; }

        public Game? Game { get; set; }
        public int? GameId { get; set; }

    }
}
