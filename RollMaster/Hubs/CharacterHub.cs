using Microsoft.AspNetCore.SignalR;
using RollMaster.Models;
using System.Threading.Tasks;

namespace RollMaster.Hubs
{
    public class CharacterHub : Hub
    {
        // Metoda wywoływana przez klienta po zmianie postaci
        public async Task CharacterUpdated(Character character)
        {
            // Rozgłoś aktualizację wszystkim innym w tej samej grze
            await Clients.OthersInGroup($"game-{character.GameId}")
                .SendAsync("ReceiveCharacterUpdate", character);
        }

        public async Task JoinGame(string gameId)
        {
            if (string.IsNullOrEmpty(gameId)) return;
            await Groups.AddToGroupAsync(Context.ConnectionId, $"game-{gameId}");
        }

        public async Task LeaveGame(int gameId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"game-{gameId}");
        }
    }
}