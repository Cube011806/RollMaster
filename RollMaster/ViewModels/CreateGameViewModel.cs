using RollMaster.Models;

namespace RollMaster.ViewModels
{
    public class CreateGameViewModel
    {
        public Game Game { get; set; } = new Game();

        public List<Character> Characters { get; set; } = new List<Character>();

        public List<int> SelectedCharacterIds { get; set; } = new List<int>();
    }

}
