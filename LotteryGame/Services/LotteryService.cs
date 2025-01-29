using LotteryGame.Entities;
using LotteryGame.Interfaces;

namespace LotteryGame.Services
{
    public class LotteryService
    {
        private readonly IGeneratorService _generator;
        private readonly ICalculatorService _calculator;

        public LotteryService(IGeneratorService generator, ICalculatorService calculator)
        {
            _generator = generator;
            _calculator = calculator;
        }
        public LotteryResult RunLottery(List<TierBase> tiers, List<Ticket> allTickets)
        {
            var lotteryResult = new LotteryResult();

            var totalGameRevenue = allTickets.Sum(t => t.Price);
            decimal totalDistributedRewardPerGame = 0m;

            foreach (TierBase tier in tiers)
            {
                var tierRevenue = _calculator.CalculateTierRevenue(tier.RevenueShare, totalGameRevenue);

                var winningTicketsCountCalculatedFromSettings = tier.GetWinningTicketsCount(allTickets.Count);

                var winningTickets = _generator.PickWinningTickets(winningTicketsCountCalculatedFromSettings, allTickets);

                var rewardPerWinningTicket = _calculator.CalculateRewardPerWinningTicket(tierRevenue, winningTicketsCountCalculatedFromSettings);

                var tierDistributedRevenue = _calculator.CalculateTierDistributedRevenue(rewardPerWinningTicket, winningTicketsCountCalculatedFromSettings);

                totalDistributedRewardPerGame += tierDistributedRevenue;

                //var tierResultPerUser = GetPlayerTierTotalReward(winningTickets, rewardPerWinningTicket);
                var tierResult = new TierResult()
                {
                    Tier = tier,
                    RewardPerWinningTicket = rewardPerWinningTicket,
                    WinningTickets = winningTickets,
                };
                lotteryResult.TierResults.Add(tierResult);
            }

            var houseProfit = totalGameRevenue - totalDistributedRewardPerGame;

            lotteryResult.HouseProfit = houseProfit;

            //debug, remove these 
            //Console.WriteLine($"Total bank was: {_gameSettings.Currency}{totalGameRevenue}");
            //Console.WriteLine($"Total distributed reward was: {_gameSettings.Currency}{totalDistributedRewardPerGame}");

            return lotteryResult;

        }


        //public Dictionary<int, (int winningTicketsCount, decimal totalReward)> GetPlayerTierTotalReward(List<Ticket> winningTickets, decimal tierRewardPerWinningTicket)
        //{
        //    return winningTickets
        //        .GroupBy(ticket => ticket.PlayerId)
        //        .ToDictionary(
        //            group => group.Key,
        //            group =>
        //            (
        //                WinningTicketsCount: group.Count(),
        //                TotalReward: group.Count() * tierRewardPerWinningTicket
        //            )
        //        )
        //        .OrderByDescending(kvp => kvp.Value.WinningTicketsCount)
        //        .ThenBy(kvp => kvp.Key)
        //        .ToDictionary();
        //}
    }
}
