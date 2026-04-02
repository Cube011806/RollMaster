using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RollMaster.Data;
using RollMaster.Models;
using RollMaster.Services.Character;
using System.Data.Common;

namespace RollMaster.Controllers
{
    public class CharacterController : Controller
    {
        private readonly CharacterService _CharacterService;
        private readonly UserManager<User> _userManager;
        public CharacterController(CharacterService service, UserManager<User> userManager)
        {
            _CharacterService = service;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _CharacterService.GetAllAsync());
        }
        public IActionResult DownloadJsonCharacter(int id)
        {
            var character = _CharacterService.GetById(id);

            if (character == null)
                return NotFound();

            var bytes = _CharacterService.ExportToJson(character);

            return File(bytes, "application/json", $"character_{id}.json");
        }
        public IActionResult CreateCharacter()
        {
            var model = new Character();

            model.skills = new List<Skill>
            {
                new Skill("Czujność", 0, false),
                new Skill("Pieśni", 0, false),
                new Skill("Polowanie", 0, false),
                new Skill("Respekt", 0, false),
                new Skill("Rzemioslo", 0, false),
                new Skill("Zwinnosc", 0, false),

                new Skill("Przenikliwość", 0, false),
                new Skill("Uprzejmość", 0, false),
                new Skill("Uzdrawianie", 0, false),
                new Skill("Inspiracja", 0, false),
                new Skill("Wojaczka", 0, false),
                new Skill("Wędrówka", 0, false),

                new Skill("Szukanie", 0, false),
                new Skill("Zagadki", 0, false),
                new Skill("Rekonesans", 0, false),
                new Skill("Przekonywanie", 0, false),
                new Skill("Wiedza", 0, false),
                new Skill("Skradanie", 0, false)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharacter(Character character)
        {
            string userId = _userManager.GetUserId(User);
            await _CharacterService.CreateCharacterAsync(character, userId);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CharacterDashboard(int id)
        {
            return View(await _CharacterService.GetByIdAsync(id));
        }
        public IActionResult ImportCharacter()
        {
            throw new NotImplementedException();
        }
        
        public IActionResult EditCharacter()
        {
            throw new NotImplementedException();
        }
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            await _CharacterService.DeleteAsync(id);    
            return RedirectToAction("Index");
        }
    }
}
