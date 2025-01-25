using LotteryGame.Entities;
using LotteryGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Services
{
    public class LotteryCpuGeneratorService : ILotteryCpuGeneratorService
    {
        private readonly IRandomService _random;
        private readonly GameSettings _gameSettings;

        public LotteryCpuGeneratorService(GameSettings gameSettings, IRandomService random)
        {
            _gameSettings = gameSettings;
            _random = random;
        }

        public List<Player> GenerateCpuPlayers(int maxTicketsPlayersCanBuy)
        {
            int totalPlayers = _random.Generate(_gameSettings.MinPlayersPerGame, _gameSettings.MaxPlayersPerGame);           
            
            var players = new List<Player>();

            for (int i = 2; i < totalPlayers + 1; i++) 
            {
                var randomTicketsCount = _random.Generate(_gameSettings.MinTicketsPerPlayer, maxTicketsPlayersCanBuy);
                
                var tickets = GenerateTickets(i, randomTicketsCount);

                players.Add(new Player(i, tickets,)); //fix
            }

            return players;
        }

        public List<Ticket> GenerateTickets(int playerId, int ticketsCount)
        {
            var tickets = new List<Ticket>();

            for (int i = 0; i < ticketsCount + 1; i++) 
            {
                tickets.Add(new Ticket(playerId));
            }
            return tickets;
        }
    }
}
