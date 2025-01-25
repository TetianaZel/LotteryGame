using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame
{
    public  class GameSettings
    {
        public  double TicketPrice { get; set; } = 1.0;
        public  string Currency { get; set; } = "$";
        public  int MaxTicketsPerPlayer { get; set; } = 10;
        public  int MinTicketsPerPlayer { get; set; } = 1;
        public  double PlayerInitialBalance { get; set; } = 10.0;
        public  int MinPlayersPerGame { get; set; } = 10;
        public  int MaxPlayersPerGame { get; set; } = 15;
        public  double GrandPrizeRevenueShare { get; set; } = 0.5;
        public  double SecondTierRevenueShare { get; set; } = 0.3;
        public  double ThirdTierRevenueShare { get; set; } = 0.1;
        public  int GrandPrizeWinningTickets { get; set; } = 1;
        public  double SecondTierWinningTicketsPercentage { get; set; } = 10.0;
        public  double ThirdTierWinningTicketsPercentage { get; set; } = 20.0;
    }
}
