﻿using LotteryGame.Enums;
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
        public string Name { get; set; }
        public decimal RevenueShare { get; set; }
        public decimal WinningTicketsShare { get; set; }

        public Tier(PrizeTier type, decimal revenueShare, decimal winningTicketsShare)
        {
            Type = type;
            RevenueShare = revenueShare;
            WinningTicketsShare = winningTicketsShare;
        }

        public virtual int GetWinningTicketsNumber(int totalTicketsCount)
        {
            return (int)Math.Floor(totalTicketsCount * WinningTicketsShare);
        }

    }
}
