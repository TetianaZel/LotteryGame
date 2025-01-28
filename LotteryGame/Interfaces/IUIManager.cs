using LotteryGame.Entities;
using LotteryGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Interfaces
{
    public interface IUIManager
    {
        void ShowGreetingMessage(int maxTicketsPlayerCanBuy);
        int GetPlayerInputTickets(int maxTicketsPlayerCanBuy);
        void DisplayAllPlayersAndPurchases(List<Player> players);
        public void DisplayDrawResultsForTier(List<KeyValuePair<int, (int winningTicketsCount, decimal totalReward)>> tierResults, PrizeTier type, decimal rewardPerWinningTicket);

        void DisplayHouseRevenue(decimal houseRevenue);

    }
}
