namespace RollMaster.Models
{
    public class Character
    {
        public int Id { get; set; }

        //Informacje podstawowe
        public string Imie { get; set; }
        public string RodzimaKultura { get; set; }
        public string Powolanie { get; set; }
        public int Wiek { get; set; }
        public string PoziomZycia { get; set; }
        public string Patron { get; set; }
        public string SciezkaCienia { get; set; }
        public int Skarb { get; set; }
        public string Wyrozniki { get; set; }
        public string Przywary { get; set; }

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

        //Umiejetnosci Siły
        public int Czujnosc { get; set; }
        public int Piesni { get; set; }
        public int Polowanie { get; set; }
        public int Respekt { get; set; }
        public int Rzemioslo { get; set; }
        public int Zwinnosc { get; set; }

        //Wzmocnienia Umiejetnosci Siły
        public bool WCzujnosc { get; set; }
        public bool WPiesni { get; set; }
        public bool WPolowanie { get; set; }
        public bool WRespekt { get; set; }
        public bool WRzemioslo { get; set; }
        public bool WZwinnosc { get; set; }

        //Umiejetnosci Serca
        public int Przenikliwosc { get; set; }
        public int Uprzejmosc { get; set; }
        public int Uzdrawianie { get; set; }
        public int Inspiracja { get; set; }
        public int Wojaczka { get; set; }
        public int Wedrowka { get; set; }

        //Wzmocnienia Umiejetnosci Serca
        public bool WPrzenikliwosc { get; set; }
        public bool WUprzejmosc { get; set; }
        public bool WUzdrawianie { get; set; }
        public bool WInspiracja { get; set; }
        public bool WWojaczka { get; set; }
        public bool WWedrowka { get; set; }

        //Umiejetnosci Rozumu
        public int Szukanie { get; set; }
        public int Zagadki { get; set; }
        public int Rekonesans { get; set; }
        public int Przekonywanie { get; set; }
        public int Wiedza { get; set; }
        public int Skradanie { get; set; }

        //Wzmocnienia Umiejetnosci Rozumu
        public bool WSzukanie { get; set; }
        public bool WZagadki { get; set; }
        public bool WRekonesans { get; set; }
        public bool WPrzekonywanie { get; set; }
        public bool WWiedza { get; set; }
        public bool WSkradanie { get; set; }
        
        //Biegłosci Bojowe
        public int BBLuki { get; set; }
        public int BBMiecze { get; set; }
        public int BBTopory { get; set; }
        public int BBWlocznie { get; set; }

        //Nagrody
        public int Mestwo { get; set; }
        public string Nagrody { get; set; }

        //Przymioty
        public int Madrosc { get; set; }
        public string Przymioty { get; set; }

        //Rynsztunek
        //R1
        public string R1Nazwa { get; set; }
        public int R1Obrazenia { get; set; }
        public int R1Przebicie { get; set; }
        public int R1Obciazenie { get; set; }
        public string R1Uwagi { get; set; }
        //R2
        public string R2Nazwa { get; set; }
        public int R2Obrazenia { get; set; }
        public int R2Przebicie { get; set; }
        public int R2Obciazenie { get; set; }
        public string R2Uwagi { get; set; }
        //R3
        public string R3Nazwa { get; set; }
        public int R3Obrazenia { get; set; }
        public int R3Przebicie { get; set; }
        public int R3Obciazenie { get; set; }
        public string R3Uwagi { get; set; }
        //R4
        public string R4Nazwa { get; set; }
        public int R4Obrazenia { get; set; }
        public int R4Przebicie { get; set; }
        public int R4Obciazenie { get; set; }
        public string R4Uwagi { get; set; }
        //Zbroja
        public string ZbrojaNazwa { get; set; }
        public int ZbrojaPancerz { get; set; }
        public int ZbrojaObciazenie { get; set; }
        //Hełm
        public string HelmNazwa { get; set; }
        public int HelmPancerz { get; set; }
        public int HelmObciazenie { get; set; }
        //Tarcza
        public string TarczaNazwa { get; set; }
        public int TarczaObrona { get; set; }
        public int TarczaObciazenie { get; set; }

        //Rozwój
        public int PunktyPrzygody { get; set; }
        public int PunktyUmiejetnosci { get; set; }
        public int PunktyZazylosci { get; set; }

        //Obecna wytrzymałość
        public int ObecnaWytrzymalosc { get; set; }
        public int Obciazenie { get; set; }
        public int Znuzenie { get; set; }

        //Obecna nadzieja
        public int ObecnaNadzieja { get; set; }
        public int Cien { get; set; }
        public int PietnoCienia { get; set; }

        //Ograniczenia
        public bool Wyczerpanie { get; set; }
        public bool Przygnebienie { get; set; }
        public bool Rana { get; set; }
        public string StopienRany { get; set; }

        //Ekwipunek
        public string Ekwipunek { get; set; }

        //Dodatkowe
        public string SzczegolnaSympatia { get; set; }

        /*----------------
        -----RELACJE-----
        ----------------*/
        public User User { get; set; }
        public string? UserId { get; set; }

    }
}
