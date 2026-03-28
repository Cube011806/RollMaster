using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RollMaster.Data;
using RollMaster.Models;
using System.Data.Common;

namespace RollMaster.Controllers
{
    public class CharacterController : Controller
    {
        ApplicationDbContext _dbContext;
        UserManager<User> _userManager;
        public CharacterController(ApplicationDbContext DbContext, UserManager<User> UserManager) {
            _dbContext = DbContext;
            _userManager = UserManager;
        }

        public IActionResult Index()
        {
            var characters = _dbContext.Character.ToList();
            return View(characters);
        }
        public IActionResult DownloadJsonCharacter(int id)
        {
            var character = _dbContext.Character
                .FirstOrDefault(c => c.Id == id);

            if (character == null)
                return NotFound();

            var json = System.Text.Json.JsonSerializer.Serialize(
                character,
                new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true
                });

            var bytes = System.Text.Encoding.UTF8.GetBytes(json);

            return File(bytes, "application/json", $"character_{id}.json");
        }

        public IActionResult CreateCharacter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCharacter(Character character)
        {
            character.UserId = _userManager.GetUserId(User);
            _dbContext.Character.Add(character);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult ImportCharacter()
        {
            throw new NotImplementedException();
        }
        public IActionResult DetailsCharacter()
        {
            throw new NotImplementedException();
        }
        public IActionResult EditCharacter()
        {
            throw new NotImplementedException();
        }
        public IActionResult DeleteCharacter()
        {
            throw new NotImplementedException();
        }
    }
}
