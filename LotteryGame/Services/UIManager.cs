using ConsoleTables;
using LotteryGame.Entities;
using LotteryGame.Interfaces;
using Microsoft.Extensions.Options;

namespace LotteryGame.Services
{
    public class UIManager : IUIManager
    {
        private readonly GameSettings _gameSettings;

        public UIManager(IOptions<GameSettings> gameSettings)
        {
            _gameSettings = gameSettings.Value;
        }

        public void ShowGreetingMessage(int maxTicketsAffordable)
        {
            int maxTicketsPlayerCanBuy = Math.Min(maxTicketsAffordable, _gameSettings.MaxTicketsPerPlayer);

            Console.WriteLine("Welcome to the Lottery Game, Player 1!");
            Console.WriteLine();
            Console.WriteLine($"* Your digital balance: {_gameSettings.Currency}{_gameSettings.PlayerInitialBalance}");
            Console.WriteLine($"* Ticket Price: {_gameSettings.Currency}{_gameSettings.TicketPrice} each");
            Console.WriteLine();
            Console.WriteLine($"You can buy minimum {_gameSettings.MinTicketsPerPlayer} and maximum {maxTicketsPlayerCanBuy} tickets");
            Console.WriteLine("How many tickets do you want to buy, Player 1?");
        }

        public int GetPlayerInputTickets(int maxTicketsPlayerCanBuy)
        {
            Console.WriteLine();
            int tickets = 0;
            bool validInput = false;

            while (!validInput)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out tickets))
                {
                    if (tickets >= _gameSettings.MinTicketsPerPlayer && tickets <= maxTicketsPlayerCanBuy)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid input. Please enter a number between {_gameSettings.MinTicketsPerPlayer} and {maxTicketsPlayerCanBuy}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number of tickets you can afford.");
                }
            }

            return tickets;
        }

        public void DisplayAllPlayersAndPurchases(List<Player> players)
        {
            Console.WriteLine();
            Console.WriteLine("Other CPU players also have purchased tickets.");
            Console.WriteLine();

            var sortedPlayers = players.OrderBy(player => player.Id);

            var table = new ConsoleTable("Player ID", "Tickets Purchased");
            foreach (var player in sortedPlayers)
            {
                table.AddRow(player.Id, player.Tickets.Count);
            }
            table.Write(Format.Minimal);

            Console.WriteLine();
            Console.WriteLine("Ticket Draw Results:");
            Console.WriteLine();
        }


        public void DisplayDrawResultsForTier(Dictionary<int, (int winningTicketsCount, decimal totalReward)> tierResults, string tierName, decimal rewardPerWinningTicket)
        {
            Console.WriteLine($"* {tierName} - Reward for a winning ticket is {_gameSettings.Currency}{rewardPerWinningTicket}.");

            var table = new ConsoleTable("Player", "How many tickets have won", "Total reward");
            foreach (var result in tierResults)
            {
                table.AddRow(result.Key, result.Value.winningTicketsCount, result.Value.totalReward);
            }
            table.Write(Format.Minimal);
            Console.WriteLine();

        }

        public void ShowResult(LotteryResult result)
        {
            foreach (var tierResult in result.TierResults)
            {
                ShowTierResult(tierResult);
            }
            DisplayHouseRevenue(result.HouseProfit);
        }

        private void ShowTierResult(TierResult tierResult)
        {
            Console.WriteLine($"* {tierResult.Tier.Name} - Reward for a winning ticket is {_gameSettings.Currency}{tierResult.RewardPerWinningTicket}.");

            var table = new ConsoleTable("Player", "How many tickets have won", "Total reward");

            foreach (var userTickets in tierResult.WinningTickets.GroupBy(t => t.PlayerId)
                .OrderByDescending(g => g.Count()).ThenBy(g => g.Key))
            {
                var winningTikets = userTickets.Count();
                table.AddRow(userTickets.Key, winningTikets, winningTikets * tierResult.RewardPerWinningTicket);
            }
            table.Write(Format.Minimal);
            Console.WriteLine();
        }

        public void DisplayHouseRevenue(decimal houseRevenue)
        {
            Console.WriteLine("Congratulations to the winners!");
            Console.WriteLine();
            Console.WriteLine($"House Revenue: {_gameSettings.Currency}{houseRevenue}");
        }
    }
}
