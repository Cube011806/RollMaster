namespace RollMaster.Models
{
    public class BodyArmor : Armor
    {
        public int? Pancerz { get; set; }
        //Relacje
        public int CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
