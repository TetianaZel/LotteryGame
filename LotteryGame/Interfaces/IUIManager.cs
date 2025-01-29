using LotteryGame.Entities;

namespace LotteryGame.Interfaces
{
    public interface IUIManager
    {
        void ShowGreetingMessage(int maxTicketsPlayerCanBuy);
        int GetPlayerInputTickets(int maxTicketsPlayerCanBuy);
        void DisplayAllPlayersAndPurchases(List<Player> players);
        void DisplayHouseRevenue(decimal houseRevenue);
        void ShowResult(LotteryResult result);

    }
}
