using LotteryGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Entities
{
    public class GrandPrizeTier : Tier
    {
        public int GrandPrizeWinningTickets { get; set; }

        public GrandPrizeTier(decimal revenueShare, int grandPrizeWinningTickets) : base(PrizeTier.GrandPrize, revenueShare, 0) //fix 0
        {
            GrandPrizeWinningTickets = grandPrizeWinningTickets;
        }

        public override int GetWinningTicketsNumber(int totalTicketsCount)
        {
            return GrandPrizeWinningTickets;
        }
    }
}
