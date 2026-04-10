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
            return await _context.Characters.Include(u => u.User).ToListAsync();
        }

        public async Task<List<Models.Character>> GetAllUserAsync(string userId)
        {
            return await _context.Characters
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<Models.Character?> GetByIdAsync(int id)
        {
            var character = await _context.Characters
                .Include(c => c.Skills)
                .Include(c => c.Weapons)
                .Include(c => c.Zbroja)
                .Include(c => c.Helm)
                .Include(c => c.Tarcza)
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

            if (character.Weapons != null)
            {
                character.Weapons = character.Weapons
                    .Where(w => !string.IsNullOrWhiteSpace(w.Name))
                    .ToList();
            }

            if (character.Zbroja?.Nazwa == null)
                character.Zbroja = null;

            if (character.Helm?.Nazwa == null)
                character.Helm = null;

            if (character.Tarcza?.Nazwa == null)
                character.Tarcza = null;

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Models.Character model)
        {
            var character = await _context.Characters
                .Include(c => c.Zbroja)
                .Include(c => c.Helm)
                .Include(c => c.Tarcza)
                .Include(c => c.Skills)
                .Include(c => c.Weapons)
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (character == null) return;

            // --- proste pola ---
            _context.Entry(character).CurrentValues.SetValues(model);
            _context.Entry(character).Property(x => x.GameId).IsModified = false; //Naprawa gubienia GameId - do sprawdzenia.
            _context.Entry(character).Property(x => x.UserId).IsModified = false;

            if (model.Zbroja != null)
            {
                if (character.Zbroja == null)
                    character.Zbroja = new BodyArmor();

                character.Zbroja.Nazwa = model.Zbroja.Nazwa;
                character.Zbroja.Pancerz = model.Zbroja.Pancerz;
                character.Zbroja.Obciazenie = model.Zbroja.Obciazenie;
            }
            if (model.Helm != null)
            {
                if (character.Helm == null)
                    character.Helm = new Helmet();

                character.Helm.Nazwa = model.Helm.Nazwa;
                character.Helm.Pancerz = model.Helm.Pancerz;
                character.Helm.Obciazenie = model.Helm.Obciazenie;
            }
            if (model.Tarcza != null)
            {
                if (character.Tarcza == null)
                    character.Tarcza = new Shield();

                character.Tarcza.Nazwa = model.Tarcza.Nazwa;
                character.Tarcza.Obrona = model.Tarcza.Obrona;
                character.Tarcza.Obciazenie = model.Tarcza.Obciazenie;
            }

            // --- SKILLS ---
            foreach (var skill in model.Skills)
            {
                var existing = character.Skills.FirstOrDefault(s => s.Id == skill.Id);
                if (existing != null)
                {
                    existing.Value = skill.Value;
                    existing.IsEnhanced = skill.IsEnhanced;
                }
            }

            // --- WEAPONS ---
            foreach (var weapon in model.Weapons)
            {
                var existing = character.Weapons.FirstOrDefault(w => w.Id == weapon.Id);
                if (existing != null)
                {
                    existing.Name = weapon.Name;
                    existing.Damage = weapon.Damage;
                    existing.Injury = weapon.Injury;
                    existing.Load = weapon.Load;
                    existing.Notes = weapon.Notes;
                }
                else
                {
                    character.Weapons.Add(weapon);
                }
            }

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
