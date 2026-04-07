using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RollMaster.Data;
using RollMaster.Models;
using RollMaster.Services.Character;
using RollMaster.Services.Game;
using RollMaster.ViewModels;

namespace RollMaster.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _gameService;
        private readonly CharacterService _characterService;
        private readonly UserManager<User> _userManager;
        public GameController(GameService GameService, CharacterService CharacterService, UserManager<User> userManager)
        {
            _gameService = GameService;
            _characterService = CharacterService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _gameService.GetAllAsync());
        }

        public async Task<IActionResult> CreateGame()
        {
            var vm = new CreateGameViewModel
            {
                Characters = await _characterService.GetAllAsync()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateGame(CreateGameViewModel vm)
        {
            _gameService.CreateGame(vm.Game, vm.SelectedCharacterIds);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> PlayerDashboardAsync(int id)
        {
            return View(await _gameService.GetGameByIdAsync(id));
        }
        public async Task<IActionResult> GMDashboard(int id)
        {
            var game = await _gameService.GetGameWithCharactersAsync(id);
            return View(game);
        }
        [HttpPost]
        public async Task<IActionResult> GMDashboard(Game model)
        {
            await _gameService.UpdateGameAsync(model);
            return RedirectToAction("GMDashboard", new { id = model.Id });
        }
    }
}
