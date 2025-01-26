using LotteryGame.Entities;
using LotteryGame.Enums;
using LotteryGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly GameSettings _gameSettings;

        public CalculatorService(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public decimal CalculateTotalRevenue(List<Player> players)
        {
            return players.Sum(player => player.Tickets.Count) * _gameSettings.TicketPrice; //update total revenue in tier
        }

        public decimal CalculateTierRevenue(Tier tier, decimal totalRevenue)
        {
            //validation needed?
            tier.TierRevenue = totalRevenue * tier.RevenueShare;   //should be separate method to update revenue not in this class

            return tier.TierRevenue;
        }

        public int CalculateWinningTicketsNumber(Tier tier, int totalTicketsCount)
        {
            if (tier.Type == PrizeTier.GrandPrize)
            {
                return _gameSettings.GrandPrizeWinningTickets; //update  in tier
            }

            else
            {
                return (int)Math.Floor(totalTicketsCount * tier.WinningTicketsShare);
            }
        }

        public decimal CalculateRewardPerWinner(decimal revenue, int winningTicketsCount)
        {
            return Math.Floor(revenue / winningTicketsCount * 100) / 100; //update in tier
        }

        public decimal CalculateTierDistributedRevenue(decimal rewardPerWinner, int winningTicketsCount)
        {
            return rewardPerWinner * winningTicketsCount;
        }
    }
}
