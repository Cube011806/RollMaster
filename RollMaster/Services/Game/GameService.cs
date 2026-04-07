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
            return await _context.Games.ToListAsync();
        }
        public async Task<Models.Game> GetGameByIdAsync(int id)
        {
            return await _context.Games.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void CreateGame(Models.Game game, List<int> characterIds)
        {
            _context.Games.Add(game);
            _context.SaveChanges();

            foreach (var id in characterIds)
            {
                var character = _context.Characters.FirstOrDefault(c => c.Id == id);
                if (character != null)
                {
                    character.GameId = game.Id;
                }
            }

            _context.SaveChanges();
        }
        public async Task<Models.Game> GetGameWithCharactersAsync(int id)
        {
            var games = _context.Games
                .Include(g => g.Characters)
                    .ThenInclude(c => c.Zbroja)
                .Include(g => g.Characters)
                    .ThenInclude(c => c.Helm)
                .Include(g => g.Characters)
                    .ThenInclude(c => c.Tarcza)
                .Include(g => g.Characters)
                    .ThenInclude(c => c.Skills)
                .Include(g => g.Characters)
                    .ThenInclude(c => c.Weapons)
                .FirstOrDefaultAsync(g => g.Id == id);
            return await games;
        }
        public async Task UpdateGameAsync(Models.Game model)
        {
            var game = await _context.Games
                .Include(g => g.Characters)
                .FirstOrDefaultAsync(g => g.Id == model.Id);

            if (game == null) return;

            game.Name = model.Name;
            game.Description = model.Description;

            foreach (var chModel in model.Characters)
            {
                var ch = game.Characters.FirstOrDefault(c => c.Id == chModel.Id);
                if (ch != null)
                {
                    ch.Wiek = chModel.Wiek;
                    ch.PoziomZycia = chModel.PoziomZycia;
                    ch.Skarb = chModel.Skarb;

                    ch.WartoscSila = chModel.WartoscSila;
                    ch.WartoscSerce = chModel.WartoscSerce;
                    ch.WartoscRozum = chModel.WartoscRozum;

                    ch.ObecnaWytrzymalosc = chModel.ObecnaWytrzymalosc;
                    ch.ObecnaNadzieja = chModel.ObecnaNadzieja;

                    ch.Wyczerpanie = chModel.Wyczerpanie;
                    ch.Przygnebienie = chModel.Przygnebienie;
                    ch.Rana = chModel.Rana;
                    ch.StopienRany = chModel.StopienRany;

                    ch.Ekwipunek = chModel.Ekwipunek;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
