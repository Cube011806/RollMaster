
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RollMaster.Data;
using RollMaster.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace RollMaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Character? lastCharacter = null;

            if (!string.IsNullOrEmpty(userId))
            {
                var characterId = await _context.Users
                    .AsNoTracking()
                    .Where(u => u.Id == userId)
                    .Select(u => u.LastPlayedCharacterId)
                    .FirstOrDefaultAsync();

                if (characterId.HasValue)
                {
                    lastCharacter = await _context.Characters
                        .AsNoTracking()
                        .FirstOrDefaultAsync(c =>
                            c.Id == characterId.Value &&
                            c.UserId == userId);
                }
            }

            ViewBag.LastPlayedCharacter = lastCharacter;

            return View();
        }


        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}
