using LotteryGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Entities
{
    public abstract class TierBase
    {
        public string Name { get; set; }
        public decimal RevenueShare { get; set; }
        public abstract int GetWinningTicketsCount(int totalTicketsCount);
    }
}
