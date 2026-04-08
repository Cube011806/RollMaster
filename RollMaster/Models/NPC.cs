namespace RollMaster.Models
{
    public class NPC
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty; //Imię
        public int Endurance { get; set; } //Wytrzymałość
        public int Might { get; set; } //Potęga
        public int Hate { get; set; } //Nienawiść lub Determinacja (w zależności od IsHate)
        public int Parry { get; set; } //Obrona
        public int Armour { get; set; } //Pancerz
        public int Attribute { get; set; } //Zajadłość
        public bool IsHate { get; set; }
        public string CombatAbilites { get; set; } = String.Empty; // Biegłości Bojowe
        public string DarkAttributes { get; set; } = String.Empty; //Mroczne Atrybuty

        //Brakuje relacji

    }
}
