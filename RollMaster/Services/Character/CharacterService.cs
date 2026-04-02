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
            return await _context.Character.ToListAsync();
        }

        public async Task<Models.Character?> GetByIdAsync(int id)
        {
            var character = await _context.Character
                .Include(c => c.skills)
                .FirstOrDefaultAsync(c => c.Id == id);
            return character;
        }
        public Models.Character? GetById(int id)
        {
            return _context.Character.FirstOrDefault(c => c.Id == id);
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

            _context.Character.Add(character);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var character = await _context.Character.FindAsync(id);
            if (character == null) return;

            _context.Character.Remove(character);
            await _context.SaveChangesAsync();
        }
    }
}
