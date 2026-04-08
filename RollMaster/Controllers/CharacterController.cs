using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RollMaster.Data;
using RollMaster.Hubs;
using RollMaster.Models;
using RollMaster.Services.Character;
using System.Data.Common;

namespace RollMaster.Controllers
{
    public class CharacterController : Controller
    {
        private readonly CharacterService _CharacterService;
        private readonly UserManager<User> _userManager;
        private readonly IHubContext<CharacterHub> _hubContext;

        public CharacterController(CharacterService service, UserManager<User> userManager, IHubContext<CharacterHub> hubContext)
        {
            _CharacterService = service;
            _userManager = userManager;
            _hubContext = hubContext;
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

            model.Skills = new List<Skill>
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
                new Skill("Skradanie", 0, false),

                new Skill("Łuki", 0, false),
                new Skill("Miecze", 0, false),
                new Skill("Topory", 0, false),
                new Skill("Włócznie", 0, false)
            };
            model.Weapons = new List<Weapon>
            {
                new Weapon(),
                new Weapon(),
                new Weapon(),
                new Weapon()
            };
            model.Zbroja = new BodyArmor();
            model.Helm = new Helmet();
            model.Tarcza = new Shield();
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

        [HttpPost]
        public async Task<IActionResult> SaveCharacter(Character model)
        {
            await _CharacterService.UpdateAsync(model);

            // 🔥 WYŚLIJ UPDATE DO INNYCH
            await _hubContext.Clients.Group($"game-{model.GameId}")
                .SendAsync("ReceiveCharacterUpdate", model);

            return Ok();
        }
        public async Task<IActionResult> CharacterPartial(int id)
        {
            var character = await _CharacterService.GetByIdAsync(id);
            return PartialView("_CharacterDashboard", character);
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
