namespace RollMaster.Models
{
    public class Character
    {
        public int Id { get; set; }

        //Informacje podstawowe
        public string Imie { get; set; } = String.Empty;
        public string RodzimaKultura { get; set; } = String.Empty;
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

        public IList<Skill> skills { get; set; } = new List<Skill>();

        //Umiejetnosci Siły (LEGACY)
        //public int Czujnosc { get; set; } = 0;
        //public int Piesni { get; set; } = 0;
        //public int Polowanie { get; set; } = 0;
        //public int Respekt { get; set; } = 0;
        //public int Rzemioslo { get; set; } = 0;
        //public int Zwinnosc { get; set; } = 0;

        //Wzmocnienia Umiejetnosci Siły (LEGACY)
        //public bool WCzujnosc { get; set; } = false;
        //public bool WPiesni { get; set; } = false;
        //public bool WPolowanie { get; set; } = false;
        //public bool WRespekt { get; set; } = false;
        //public bool WRzemioslo { get; set; } = false;
        //public bool WZwinnosc { get; set; } = false;

        //Umiejetnosci Serca (LEGACY)
        //public int Przenikliwosc { get; set; } = 0;
        //public int Uprzejmosc { get; set; } = 0;
        //public int Uzdrawianie { get; set; } = 0;
        //public int Inspiracja { get; set; } = 0;
        //public int Wojaczka { get; set; } = 0;
        //public int Wedrowka { get; set; } = 0;

        //Wzmocnienia Umiejetnosci Serca (LEGACY)
        //public bool WPrzenikliwosc { get; set; } = false;
        //public bool WUprzejmosc { get; set; } = false;
        //public bool WUzdrawianie { get; set; } = false;
        //public bool WInspiracja { get; set; } = false;
        //public bool WWojaczka { get; set; } = false;
        //public bool WWedrowka { get; set; } = false;

        //Umiejetnosci Rozumu (LEGACY)
        //public int Szukanie { get; set; } = 0;
        //public int Zagadki { get; set; } = 0;
        //public int Rekonesans { get; set; } = 0;
        //public int Przekonywanie { get; set; } = 0;
        //public int Wiedza { get; set; } = 0;
        //public int Skradanie { get; set; } = 0;

        //Wzmocnienia Umiejetnosci Rozumu (LEGACY)
        //public bool WSzukanie { get; set; } = false;
        //public bool WZagadki { get; set; } = false;
        //public bool WRekonesans { get; set; } = false;
        //public bool WPrzekonywanie { get; set; } = false;
        //public bool WWiedza { get; set; } = false;
        //public bool WSkradanie { get; set; } = false;

        //Biegłosci Bojowe
        public int BBLuki { get; set; } = 0;
        public int BBMiecze { get; set; } = 0;
        public int BBTopory { get; set; } = 0;
        public int BBWlocznie { get; set; } = 0;

        //Nagrody
        public int Mestwo { get; set; } = 0;
        public string? Nagrody { get; set; } = String.Empty;

        //Przymioty
        public int Madrosc { get; set; } = 0;
        public string? Przymioty { get; set; } = String.Empty;

        //Rynsztunek
        //R1
        public string? R1Nazwa { get; set; }
        public int? R1Obrazenia { get; set; }
        public int? R1Przebicie { get; set; }
        public int? R1Obciazenie { get; set; }
        public string? R1Uwagi { get; set; }
        //R2
        public string? R2Nazwa { get; set; }
        public int? R2Obrazenia { get; set; }
        public int? R2Przebicie { get; set; }
        public int? R2Obciazenie { get; set; }
        public string? R2Uwagi { get; set; }
        //R3
        public string? R3Nazwa { get; set; }
        public int? R3Obrazenia { get; set; }
        public int? R3Przebicie { get; set; }
        public int? R3Obciazenie { get; set; }
        public string? R3Uwagi { get; set; }
        //R4
        public string? R4Nazwa { get; set; }
        public int? R4Obrazenia { get; set; }
        public int? R4Przebicie { get; set; }
        public int? R4Obciazenie { get; set; }
        public string? R4Uwagi { get; set; }
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
