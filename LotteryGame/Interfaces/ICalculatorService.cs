using LotteryGame.Entities;

namespace LotteryGame.Interfaces
{
    public interface ICalculatorService
    {
        decimal CalculateTotalRevenue(List<Player> players);
        decimal CalculateTierRevenue(decimal revenueShare, decimal totalRevenue);
        decimal CalculateRewardPerWinningTicket(decimal revenue, int winningTicketsCount);
        decimal CalculateTierDistributedRevenue(decimal rewardPerWinner, int winningTicketsCount);
    }
}
