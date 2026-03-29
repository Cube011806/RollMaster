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
            return View();
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
