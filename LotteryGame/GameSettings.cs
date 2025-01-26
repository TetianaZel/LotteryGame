using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame
{
    public  class GameSettings
    {
        public  decimal TicketPrice { get; set; } = 1.0m;
        public  string Currency { get; set; } = "$";
        public  int MaxTicketsPerPlayer { get; set; } = 10;
        public  int MinTicketsPerPlayer { get; set; } = 1;
        public  decimal PlayerInitialBalance { get; set; } = 10.0m;
        public  int MinPlayersPerGame { get; set; } = 10;
        public  int MaxPlayersPerGame { get; set; } = 15;
        public  decimal GrandPrizeRevenueShare { get; set; } = 0.5m;
        public  decimal SecondTierRevenueShare { get; set; } = 0.3m;
        public  decimal ThirdTierRevenueShare { get; set; } = 0.1m;
        public  int GrandPrizeWinningTickets { get; set; } = 1;
        public  decimal SecondTierWinningTicketsShare { get; set; } = 0.1m;
        public  decimal ThirdTierWinningTicketsShare { get; set; } = 0.2m;
    }
}
