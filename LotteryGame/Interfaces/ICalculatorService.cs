using LotteryGame.Entities;
using LotteryGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Interfaces
{
    public interface ICalculatorService
    {
        decimal CalculateTotalRevenue(List<Player> players);

        decimal CalculateTierRevenue(Tier tier, decimal totalRevenue);
        decimal CalculateRewardPerWinningTicket(decimal revenue, int winningTicketsCount);
        decimal CalculateTierDistributedRevenue(decimal rewardPerWinner, int winningTicketsCount);
    }
}
