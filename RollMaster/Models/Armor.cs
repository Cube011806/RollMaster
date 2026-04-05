namespace RollMaster.Models
{
    public abstract class Armor
    {
        public int Id { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public int Obciazenie { get; set; }

    }
}
