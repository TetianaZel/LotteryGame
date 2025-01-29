using LotteryGame.Entities;
using LotteryGame.Interfaces;
using LotteryGame.Services;

namespace LotteryGame
{
    internal class LotteryApplication
    {
        private readonly LotteryService lotteryService;
        private readonly LotteryInitializatorService lotteryInitializatorService;
        private readonly IUIManager ui;

        public LotteryApplication(
            LotteryService lotteryService,
            LotteryInitializatorService lotteryInitializatorService,
            IUIManager ui
            )
        {
            this.lotteryService = lotteryService;
            this.lotteryInitializatorService = lotteryInitializatorService;
            this.ui = ui;
        }

        public void Start()
        {
            var maxTicketsPlayerCanBuy = lotteryInitializatorService.GetMaxTicketsPlayerCanBuy();
            ui.ShowGreetingMessage(maxTicketsPlayerCanBuy);
            var playerTiketsCount = ui.GetPlayerInputTickets(maxTicketsPlayerCanBuy);

            (List<TierBase> tiers, List<Ticket> allTickets, List<Player> players) = lotteryInitializatorService.InitializeGame(playerTiketsCount, maxTicketsPlayerCanBuy);
            ui.DisplayAllPlayersAndPurchases(players);
            var lotteryResult = lotteryService.RunLottery(tiers, allTickets);
            ui.ShowResult(lotteryResult);
        }
    }
}
