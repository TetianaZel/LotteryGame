using LotteryGame.Entities;
using LotteryGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Interfaces
{
    public interface ILotteryCalculatorService
    {
        double CalculateTotalRevenue(List<Player> players);

        //public double GetGrandPrizePot();

        //public double GetSecondTierPrizePot();
        //public double GetThirdTierPrizePot();
        //public double GetHouseProfit();
        double GetPotForPrizeTier(PrizeTier prizeTier, double totalRevenue);
    }
}
