using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RollMaster.Data;

namespace RollMaster.Controllers
{
    public class GameController : Controller
    {
        ApplicationDbContext _dbContext;
        public GameController(ApplicationDbContext DbContext)
        {
            _dbContext = DbContext;
        }
        public IActionResult Index()
        {
            var games = _dbContext.Game.ToList();
            return View(games);
        }
    }
}
