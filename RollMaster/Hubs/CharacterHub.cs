using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RollMaster.Data;
using RollMaster.Models;

namespace RollMaster.Hubs
{
    public class CharacterHub : Hub
    {
        private readonly ApplicationDbContext _db;

        public CharacterHub(ApplicationDbContext db)
        {
            _db = db;
        }

        private async Task<bool> CanAccessGameAsync(int gameId)
        {
            if (Context.User?.Identity?.IsAuthenticated != true)
                return false;

            var userId = Context.UserIdentifier;

            if (string.IsNullOrEmpty(userId))
                return false;

            var user = await _db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            var gameExists = await _db.Games
                .AnyAsync(g => g.Id == gameId);

            if (!gameExists)
                return false;

            // MG ma dostęp do gier w aplikacji.
            if (user.AccessLevel == 1)
                return true;

            // Gracz musi mieć własną postać w tej grze.
            return await _db.Characters.AnyAsync(c =>
                c.GameId == gameId &&
                c.UserId == userId);
        }

        public async Task JoinGame(int gameId)
        {
            if (!await CanAccessGameAsync(gameId))
                throw new HubException(
                    "Nie masz dostępu do tej gry.");

            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                $"game-{gameId}");
        }

        public async Task LeaveGame(int gameId)
        {
            await Groups.RemoveFromGroupAsync(
                Context.ConnectionId,
                $"game-{gameId}");
        }

        public async Task<List<DiceLogDto>> GetRecentDiceLogs(
            int gameId)
        {
            if (!await CanAccessGameAsync(gameId))
                throw new HubException(
                    "Nie masz dostępu do historii tej gry.");

            var logs = await _db.DiceLogs
                .AsNoTracking()
                .Where(d => d.GameId == gameId)
                .OrderByDescending(d => d.CreatedAt)
                .Take(100)
                .Select(d => new DiceLogDto(
                    d.Id,
                    d.CharacterName,
                    d.Description,
                    d.CreatedAt))
                .ToListAsync();

            return logs;
        }

        public async Task SendDiceLog(
            int characterId,
            string description)
        {
            if (Context.User?.Identity?.IsAuthenticated != true)
                throw new HubException(
                    "Musisz być zalogowany.");

            var userId = Context.UserIdentifier;

            if (string.IsNullOrEmpty(userId))
                throw new HubException(
                    "Nie można ustalić użytkownika.");

            description = description?.Trim() ?? string.Empty;

            if (description.Length == 0 ||
                description.Length > 500)
            {
                throw new HubException(
                    "Nieprawidłowy opis rzutu.");
            }

            var character = await _db.Characters
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == characterId);

            if (character == null || character.GameId == null)
                throw new HubException(
                    "Nie znaleziono postaci przypisanej do gry.");

            var gameId = character.GameId.Value;

            if (!await CanAccessGameAsync(gameId))
                throw new HubException(
                    "Nie masz dostępu do tej gry.");

            var user = await _db.Users
                .AsNoTracking()
                .FirstAsync(u => u.Id == userId);

            // Gracz może wykonywać rzuty tylko własną postacią.
            // MG może wykonać rzut dla dowolnej postaci w grze.
            if (user.AccessLevel != 1 &&
                character.UserId != userId)
            {
                throw new HubException(
                    "Nie możesz wykonywać rzutów tą postacią.");
            }

            var log = new DiceLog
            {
                GameId = gameId,
                CharacterName = character.Imie,
                Description = description,
                CreatedAt = DateTime.UtcNow
            };

            _db.DiceLogs.Add(log);
            await _db.SaveChangesAsync();

            var entry = new DiceLogDto(
                log.Id,
                log.CharacterName,
                log.Description,
                log.CreatedAt);

            // Wysyłamy także do autora rzutu.
            await Clients.Group($"game-{gameId}")
                .SendAsync("ReceiveDiceLog", entry);
        }

        // Dotychczasowa obsługa aktualizacji postaci.
        public async Task CharacterUpdated(Character character)
        {
            await Clients.OthersInGroup(
                    $"game-{character.GameId}")
                .SendAsync("ReceiveCharacterUpdate", character);
        }
    }

    public record DiceLogDto(
        int Id,
        string CharacterName,
        string Description,
        DateTime CreatedAt);
}