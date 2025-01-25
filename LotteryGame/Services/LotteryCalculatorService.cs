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
    public class LotteryCalculatorService : ILotteryCalculatorService
    {
        private readonly GameSettings _gameSettings;

        public LotteryCalculatorService(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public double CalculateTotalRevenue(List<Player> players)
        {
            return players.Sum(player => player.Tickets.Count) * _gameSettings.TicketPrice; //we need also exclude tickets that won gradually 
        }

        //public double GetGrandPrizePot()
        //{
        //    return TotalRevenue * _gameSettings.GrandPrizeRevenueShare;
        //}

        //public double GetSecondTierPrizePot()
        //{
        //    return TotalRevenue * _gameSettings.SecondTierRevenueShare;
        //}

        //public double GetThirdTierPrizePot()
        //{
        //    return TotalRevenue * _gameSettings.SecondTierRevenueShare;
        //}

        //public Dictionary<PrizeTier,double> GetRevenueDistributionPerPrizeTier(double totalRevenue)
        //{
        //    var prizePotsDistribution = new Dictionary<PrizeTier, double>();
        //    foreach (var tier in PrizeTier)
        //    {
        //        potPerPrizeTier[tier.Key] = TotalRevenue * tier.Value;
        //    }

        //    return potPerPrizeTier;
        //}

        public double GetPotForPrizeTier(PrizeTier prizeTier, double totalRevenue)
        {
            switch(prizeTier)
            {
                case PrizeTier.GrandPrize:
                    return totalRevenue * _gameSettings.GrandPrizeRevenueShare;
                case PrizeTier.SecondTier:
                    return totalRevenue * _gameSettings.SecondTierRevenueShare;
                case PrizeTier.ThirdTier:
                    return totalRevenue * _gameSettings.ThirdTierRevenueShare;
                default:
                    throw new ArgumentException("Invalid prize tier", nameof(prizeTier));
            }
        }

        //public double GetHouseProfit(double totalRevenue)
        //{
            
        //    //return totalRevenue - totalPrizePayout; should consider that there will be remaining added to the house propfit
        //}

        //public double GetWin(PrizeTier prizeTier, int winningTicketsCount)
        //{
        //    return Math.Round(GetSecondTierPrizePot() / ticketsCount);
        //}
    }
}
