using LotteryGame.Entities;

namespace LotteryGame.Interfaces
{
    public interface IUIManager
    {
        void ShowGreetingMessage(int maxTicketsPlayerCanBuy);
        int GetPlayerInputTickets(int maxTicketsPlayerCanBuy);
        void DisplayAllPlayersAndPurchases(List<Player> players);
        public void DisplayDrawResultsForTier(Dictionary<int, (int winningTicketsCount, decimal totalReward)> tierResults, string tierName, decimal rewardPerWinningTicket);

        void DisplayHouseRevenue(decimal houseRevenue);
        void ShowResult(LotteryResult result);

    }
}
