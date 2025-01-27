using LotteryGame.Entities;
using LotteryGame.Enums;
using LotteryGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Services
{
    public class UIManager : IUIManager
    {
        private readonly GameSettings _gameSettings;

        public UIManager(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public void ShowGreetingMessage(int maxTicketsAffordable)
        {
            int maxTicketsPlayerCanBuy = Math.Min(maxTicketsAffordable, _gameSettings.MaxTicketsPerPlayer);

            Console.WriteLine("Welcome to the Lottery Game, Player 1!");
            Console.WriteLine();
            Console.WriteLine($"* Your digital balance: {_gameSettings.Currency}{_gameSettings.PlayerInitialBalance}");
            Console.WriteLine($"* Ticket Price: {_gameSettings.Currency}{_gameSettings.TicketPrice} each");
            Console.WriteLine();
            Console.WriteLine($"You can buy minimum {_gameSettings.MinTicketsPerPlayer} and maximum {maxTicketsPlayerCanBuy}");
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

            var sortedPlayers = players.OrderBy(player => player.Id).ToList();

            Console.WriteLine("+------------+----------------------+");
            Console.WriteLine("| Player ID  | Tickets Purchased    |");
            Console.WriteLine("+------------+----------------------+");

            foreach (var player in sortedPlayers)
            {
                int ticketCount = player.Tickets.Count;
                Console.WriteLine($"| {player.Id,-10} | {ticketCount,-20} |");
            }

            Console.WriteLine("+------------+----------------------+");
            Console.WriteLine();
            Console.WriteLine("Ticket Draw Results:");
            Console.WriteLine();
        }

        public void DisplayDrawResultsForTier(Tier tier)
        {
            Console.Write($"* {FormatEnumName(tier.Type)}: ");

            if (tier.WinningPlayerIds == null || !tier.WinningPlayerIds.Any())
            {
                Console.WriteLine($"No winners for {tier.Type}.");
                return;
            }

            bool isSingleWinner = tier.WinningPlayerIds.Count == 1;

            string winners = isSingleWinner
                ? $"Player {tier.WinningPlayerIds.First()}"
                : $"Players {string.Join(", ", tier.WinningPlayerIds)}";

            string rewardMessage = $"{tier.RewardPerWinner}" + (isSingleWinner ? "" : " each");

            Console.WriteLine($"{winners} won {_gameSettings.Currency}{rewardMessage}!");

            Console.WriteLine();
        }

        public void DisplayHouseRevenue(decimal houseRevenue)
        {
            Console.WriteLine("Congratulations to the winners!");
            Console.WriteLine();
            Console.WriteLine($"House Revenue: {_gameSettings.Currency}{houseRevenue}");
        }

        private static string FormatEnumName(PrizeTier tier)
        {
            return string.Concat(tier.ToString()
                .Select((character, index) => char.IsUpper(character) && index > 0 ? $" {character}" : character.ToString()));
        }
    }
}
