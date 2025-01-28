﻿using LotteryGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Entities
{
    public class GrandPrizeTier : TierBase
    {
        public int WinningTicketsCount { get; set; }

        public override int GetWinningTicketsCount(int totalTicketsCount)
        {
            return WinningTicketsCount;
        }
    }
}
