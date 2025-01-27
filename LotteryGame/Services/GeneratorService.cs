using LotteryGame.Entities;
using LotteryGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Services
{
    public class GeneratorService : IGeneratorService
    {
        private readonly IRandomService _random;
        private readonly GameSettings _gameSettings;

        public GeneratorService(GameSettings gameSettings, IRandomService random)
        {
            _gameSettings = gameSettings;
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

                var tickets = GenerateTickets(newPlayer, randomTicketsCount);

                newPlayer.Tickets = tickets;

                cpuPlayers.Add(newPlayer);
            }

            return cpuPlayers;
        }

        public List<Ticket> GenerateTickets(Player player, int ticketsCount)
        {
            var tickets = new List<Ticket>();

            for (int i = 0; i < ticketsCount; i++) 
            {
                tickets.Add(new Ticket(player.Id));
            }
            return tickets;
        }

        public List<Ticket> PickWinningTickets(int count, List<Ticket> allTickets)
        {
            List<Ticket> filtered = allTickets.Where(ticket => !ticket.HasWon).ToList();

            if (count > filtered.Count)
            {
                throw new ArgumentException($"Requested number of winning tickets {count} exceeds the number of available tickets {allTickets}.");
            }

            ShuffleTickets(filtered);

            var winningTickets = filtered.Take(count).ToList();

            foreach (var ticket in winningTickets)
            {
                ticket.HasWon = true;
            }

            return winningTickets;

        }

        private void ShuffleTickets(List<Ticket> allTickets)
        {
            // Fisher-Yates Shuffle algorithm to randomly shuffle tickets

            for (int i = allTickets.Count - 1; i > 0; i--)
            {
                int j = _random.Generate(0, i); // Generate a random index between 0 and i
                                                // Swap tickets at indices i and j
                Ticket temp = allTickets[i];
                allTickets[i] = allTickets[j];
                allTickets[j] = temp;
            }
        }
    }
}
