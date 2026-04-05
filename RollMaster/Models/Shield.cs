namespace RollMaster.Models
{
    public class Shield : Armor
    {
        public int Obrona { get; set; }
        //Relacje
        public int CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
