using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Entities
{
    public class SecondaryTier : TierBase
    {
        public decimal WinningTicketsShare { get; set; }

        public override int GetWinningTicketsCount(int totalTicketsCount)
        {
            return (int)Math.Floor(totalTicketsCount * WinningTicketsShare);
        }
    }
}
