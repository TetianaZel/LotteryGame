﻿using LotteryGame.Entities;
using LotteryGame.Interfaces;
using Microsoft.Extensions.Options;

namespace LotteryGame.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly GameSettings _gameSettings;

        public CalculatorService(IOptions<GameSettings> gameSettings)
        {
            _gameSettings = gameSettings.Value;
        }

        public decimal CalculateTotalRevenue(List<Player> players)
        {
            return players.Sum(player => player.Tickets.Count) * _gameSettings.TicketPrice;
        }

        public decimal CalculateTierRevenue(decimal revenueShare, decimal totalRevenue)
        {
            return totalRevenue * revenueShare;
        }

        public decimal CalculateRewardPerWinningTicket(decimal revenue, int winningTicketsCount)
        {
            if (winningTicketsCount == 0)
            {
                throw new Exception("Winning tickets count in this tier is 0. Can't calculate reward per ticket");
            }

            return Math.Floor(revenue / winningTicketsCount * 100) / 100;
        }

        public decimal CalculateTierDistributedRevenue(decimal rewardPerWinner, int winningTicketsCount)
        {
            return rewardPerWinner * winningTicketsCount;
        }
    }
}
