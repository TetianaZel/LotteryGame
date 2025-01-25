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
        void DisplayAllPlayersAndPurchases();
        void DisplayDrawResults();
        void DisplayHouseRevenue(double houseRevenue);

    }
}
