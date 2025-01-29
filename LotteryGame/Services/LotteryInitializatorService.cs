using LotteryGame.Entities;
using LotteryGame.Interfaces;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace LotteryGame.Services
{
    public class LotteryInitializatorService
    {
        private readonly GameSettings _gameSettings;
        private readonly IGeneratorService _generator;

        public LotteryInitializatorService(IOptions<GameSettings> gameSettings, IGeneratorService generator)
        {
            _gameSettings = gameSettings.Value;
            _generator = generator;
        }

        public int GetMaxTicketsPlayerCanBuy()
        {
            var maxTicketsAffordableByBalance = (int)Math.Floor(_gameSettings.PlayerInitialBalance / _gameSettings.TicketPrice);
            var maxTicketsAffordable = Math.Min(maxTicketsAffordableByBalance, _gameSettings.MaxTicketsPerPlayer);
            if (maxTicketsAffordable < _gameSettings.MinTicketsPerPlayer)
            {
                throw new LotteryException("Players can't afford to buy minimum required amount of tickets. Ticket price is too high for the players' initial balance.");
            }
            return maxTicketsAffordable;
        }
        public (List<TierBase> tiers, List<Ticket> allTickets, List<Player> players) InitializeGame(int playerTiketsCount, int maxTicketsPlayerCanBuy)
        {
            List<TierBase> tiers = InitializeTiers();

            Player player1 = InitializeUIPlayer(playerTiketsCount);

            var players = _generator.GenerateCpuPlayers(maxTicketsPlayerCanBuy);

            players.Add(player1);

            List<Ticket> allTickets = new List<Ticket>();

            foreach (Player player in players)
            {
                allTickets.AddRange(player.Tickets);
            }

            return (tiers, allTickets, players);
        }

        private List<TierBase> InitializeTiers()
        {
            var jsonSerializerOptions = new JsonSerializerOptions { Converters = { new TierJsonConverter(), new DecimalJsonConverter(), new IntJsonConverter() } };

            var tiers = _gameSettings.TierSettings
                .Select(dict => JsonSerializer.Serialize(dict))
                .Select(json => JsonSerializer.Deserialize<TierBase>(json, jsonSerializerOptions))
                .ToList();
            if(tiers.Any(t => t == null)) 
            {
                throw new LotteryException("Ivalid tiers settings. Check the application configuration");
            }
            if (tiers.Sum(t => t.RevenueShare) > 1)
            {
                throw new LotteryException("Invalid tiers revenue shares. Sum of all shares must be less than 1");
            }
            return tiers;
        }

        private Player InitializeUIPlayer(int ticketsCount)
        {
            Player UIplayer = new Player(1, _gameSettings.PlayerInitialBalance);
            List<Ticket> listTicketsForUIPlayer = _generator.GenerateTickets(ticketsCount, UIplayer);

            UIplayer.Tickets = listTicketsForUIPlayer;
            return UIplayer;
        }
    }
}
