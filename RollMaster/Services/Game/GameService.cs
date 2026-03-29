using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RollMaster.Data;
using RollMaster.Models;

namespace RollMaster.Services.Game
{
    public class GameService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public GameService(ApplicationDbContext context, UserManager<User> UserManager)
        {
            _context = context;
            _userManager = UserManager;
        }
        public async Task<List<Models.Game>> GetAllAsync()
        {
            return await _context.Game.ToListAsync();
        }
        public async Task<Models.Game> GetGameByIdAsync(int id)
        {
            return await _context.Game.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void CreateGame(Models.Game game, List<int> characterIds)
        {
            _context.Game.Add(game);
            _context.SaveChanges();

            foreach (var id in characterIds)
            {
                var character = _context.Character.FirstOrDefault(c => c.Id == id);
                if (character != null)
                {
                    character.GameId = game.Id;
                }
            }

            _context.SaveChanges();
        }

    }
}
