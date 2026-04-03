using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RollMaster.Data;
using RollMaster.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RollMaster.Services.Character
{
    public class CharacterService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CharacterService(ApplicationDbContext context, UserManager<User> UserManager)
        {
            _context = context;
            _userManager = UserManager;
        }

        public async Task<List<Models.Character>> GetAllAsync()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<Models.Character?> GetByIdAsync(int id)
        {
            var character = await _context.Characters
                .Include(c => c.skills)
                .Include(c => c.weapons)
                .FirstOrDefaultAsync(c => c.Id == id);
            return character;
        }
        public Models.Character? GetById(int id)
        {
            return _context.Characters.FirstOrDefault(c => c.Id == id);
        }

        public byte[] ExportToJson(Models.Character character)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            var json = JsonSerializer.Serialize(character, options);
            return Encoding.UTF8.GetBytes(json);
        }
        public async Task CreateCharacterAsync(Models.Character character, string userId)
        {
            character.UserId = userId;

            if (character.weapons != null)
            {
                character.weapons = character.weapons
                    .Where(w => !string.IsNullOrWhiteSpace(w.Name))
                    .ToList();
            }

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null) return;

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
        }
    }
}
