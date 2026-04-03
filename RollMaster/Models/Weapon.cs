namespace RollMaster.Models
{
    public class Weapon
    {
        public Weapon() { }
        public Weapon(string Name, int Damage, int Injury, int Load) {
            this.Name = Name;
            this.Damage = Damage;
            this.Injury = Injury;
            this.Load = Load;
        }
        public Weapon(string Name, int Damage, int Injury, int Load, string Notes) {
            this.Name = Name;
            this.Damage = Damage;
            this.Injury = Injury;
            this.Load = Load;
            this.Notes = Notes;
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
        public int Injury { get; set; }
        public int Load { get; set; }
        public string? Notes { get; set; }

        //Relacja z postacią
        public int CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
