using LotteryGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Entities
{
    public class Tier
    {
        public PrizeTier Type { get; set; }
        public decimal RevenueShare { get; set; }
        public decimal WinningTicketsShare { get; set; }

        public decimal TierRevenue { get; set; }
        public int WinningTicketsNumber { get; set; }
        public List<Player> Winners { get; set; } = new();
        public decimal RewardPerWinner { get; set; }

        public decimal TierDistributedRevenue { get; set; }

        public Tier(PrizeTier type, decimal revenueShare, decimal winningTicketsShare)
        {
            Type = type;
            RevenueShare = revenueShare;
            WinningTicketsShare = winningTicketsShare;
        }

    }
}
