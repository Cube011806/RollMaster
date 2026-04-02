using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RollMaster.Models
{
    public class Skill
    {
        public Skill()
        {

        }
        public Skill(string Name, int Value, bool IsEnhanced) {
            this.Name = Name;
            this.Value = Value;
            this.IsEnhanced = IsEnhanced;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Value { get; set; }
        public bool IsEnhanced { get; set; }
        public int CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
