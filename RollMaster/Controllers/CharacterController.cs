using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RollMaster.Data;
using RollMaster.Hubs;
using RollMaster.Models;
using RollMaster.Services.Character;
using System.Data.Common;
using System.Security.Claims;

namespace RollMaster.Controllers
{
    public class CharacterController : Controller
    {
        private readonly CharacterService _CharacterService;
        private readonly UserManager<User> _userManager;
        private readonly IHubContext<CharacterHub> _hubContext;
        private readonly ApplicationDbContext _context;


        public CharacterController(CharacterService service, UserManager<User> userManager, ApplicationDbContext context,IHubContext<CharacterHub> hubContext)
        {
            _CharacterService = service;
            _userManager = userManager;
            _hubContext = hubContext;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await _CharacterService.GetAllUserAsync(userId));
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
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var character = await _CharacterService.GetByIdAsync(id);

            if (character == null)
                return NotFound();

            if (character.UserId != userId)
                return Forbid();

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Challenge();

            user.LastPlayedCharacterId = character.Id;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                return StatusCode(500, "Nie udało się zapisać ostatnio używanej postaci.");

            return View(character);
        }


        [HttpPost]
        public async Task<IActionResult> SaveCharacter(Character model)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return Unauthorized();
            }

            var currentUserId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(currentUserId))
            {
                return Unauthorized();
            }

            // Pobieramy istniejącą postać z bazy.
            var character = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (character == null)
            {
                return NotFound();
            }

            // Gracz może edytować wyłącznie własną postać.
            if (character.UserId != currentUserId)
            {
                return Forbid();
            }

            // Zachowujemy przypisanie do gry z bazy,
            // zamiast ufać GameId przesłanemu przez formularz.
            model.GameId = character.GameId;

            await _CharacterService.UpdateAsync(model);

            // Powiadom pozostałych uczestników gry o zmianie.
            if (character.GameId.HasValue)
            {
                await _hubContext.Clients
                    .Group($"game-{character.GameId.Value}")
                    .SendAsync("ReceiveCharacterUpdate", new
                    {
                        id = character.Id,
                        gameId = character.GameId
                    });
            }

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
        [HttpGet]
        public async Task<IActionResult> OtherCharacters(int currentCharacterId)
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var currentCharacter = await _context.Characters
                .AsNoTracking()
                .FirstOrDefaultAsync(c =>
                    c.Id == currentCharacterId &&
                    c.UserId == userId);

            if (currentCharacter == null || !currentCharacter.GameId.HasValue)
                return NotFound();

            var characters = await _context.Characters
                .AsNoTracking()
                .Where(c =>
                    c.GameId == currentCharacter.GameId &&
                    c.Id != currentCharacter.Id)
                .OrderBy(c => c.Imie)
                .Select(c => new
                {
                    c.Id,
                    c.Imie
                })
                .ToListAsync();

            return Json(characters);
        }

        [HttpGet]
        public async Task<IActionResult> CharacterPreview(
            int id,
            int currentCharacterId)
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var currentCharacter = await _context.Characters
                .AsNoTracking()
                .FirstOrDefaultAsync(c =>
                    c.Id == currentCharacterId &&
                    c.UserId == userId);

            if (currentCharacter == null || !currentCharacter.GameId.HasValue)
                return NotFound();

            var character = await _context.Characters
                .AsNoTracking()
                .Include(c => c.Skills)
                .Include(c => c.Weapons)
                .Include(c => c.Zbroja)
                .Include(c => c.Helm)
                .Include(c => c.Tarcza)
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.GameId == currentCharacter.GameId);

            if (character == null)
                return NotFound();

            return PartialView("_CharacterPreview", character);
        }
    }
}
