using LotteryGame.Entities;
using LotteryGame.Interfaces;
using Microsoft.Extensions.Options;

namespace LotteryGame.Services
{
    public class GeneratorService : IGeneratorService
    {
        private readonly IRandomService _random;
        private readonly GameSettings _gameSettings;

        public GeneratorService(IOptions<GameSettings> gameSettings, IRandomService random)
        {
            _gameSettings = gameSettings.Value;
            _random = random;
        }

        public List<Player> GenerateCpuPlayers(int maxTicketsPlayersCanBuy)
        {
            int totalPlayers = _random.Generate(_gameSettings.MinPlayersPerGame, _gameSettings.MaxPlayersPerGame);

            var cpuPlayers = new List<Player>();

            for (int i = 2; i < totalPlayers + 1; i++)
            {
                var randomTicketsCount = _random.Generate(_gameSettings.MinTicketsPerPlayer, maxTicketsPlayersCanBuy);

                var newPlayer = new Player(i, _gameSettings.PlayerInitialBalance);

                var tickets = GenerateTickets(randomTicketsCount, newPlayer);

                newPlayer.Tickets = tickets;

                cpuPlayers.Add(newPlayer);
            }

            return cpuPlayers;
        }

        public List<Ticket> GenerateTickets(int ticketsCount, Player player)
        {
            var tickets = new List<Ticket>();

            for (int i = 0; i < ticketsCount; i++)
            {
                tickets.Add(new Ticket(player.Id)
                {
                    Price = _gameSettings.TicketPrice,
                });
            }
            return tickets;
        }

        public List<Ticket> PickWinningTickets(int count, List<Ticket> allTickets)
        {
            List<Ticket> filtered = allTickets.Where(ticket => !ticket.HasWon).ToList();

            if (count > filtered.Count)
            {
                throw new LotteryException($"Requested number of winning tickets {count} exceeds the number of available tickets {allTickets}.");
            }

            ShuffleTickets(filtered);

            var winningTickets = filtered.Take(count).ToList();

            foreach (var ticket in winningTickets)
            {
                ticket.HasWon = true;
            }

            return winningTickets;

        }

        public void ShuffleTickets(List<Ticket> allTickets)
        {
            for (int i = allTickets.Count - 1; i > 0; i--)
            {
                int j = _random.Generate(0, i); // Generate a random index between 0 and i
                                                // Swap tickets at indexes i and j
                Ticket temp = allTickets[i];
                allTickets[i] = allTickets[j];
                allTickets[j] = temp;
            }
        }
    }
}
