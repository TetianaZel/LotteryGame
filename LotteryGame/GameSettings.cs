using LotteryGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame
{
    public class GameSettings
    {
        public  decimal TicketPrice { get; set; }
        public  string Currency { get; set; }
        public  int MaxTicketsPerPlayer { get; set; }
        public  int MinTicketsPerPlayer { get; set; }
        public  decimal PlayerInitialBalance { get; set; }
        public  int MinPlayersPerGame { get; set; }
        public  int MaxPlayersPerGame { get; set; }
        public List<Tier> Tiers { get; set; }
    }
}
