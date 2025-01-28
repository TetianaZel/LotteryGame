using LotteryGame.Entities;
using LotteryGame.Enums;
using LotteryGame.Interfaces;
using Microsoft.Extensions.Options;
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

            var sortedPlayers = players.OrderBy(player => player.Id).ToList();

            Console.WriteLine("+------------+----------------------+");
            Console.WriteLine("| Player ID  |  Tickets Purchased   |");
            Console.WriteLine("+------------+----------------------+");

            foreach (var player in sortedPlayers)
            {
                int ticketCount = player.Tickets.Count;
                Console.WriteLine($"|    {player.Id,-7} |          {ticketCount,-11} |");
            }

            Console.WriteLine("+------------+----------------------+");
            Console.WriteLine();
            Console.WriteLine("Ticket Draw Results:");
            Console.WriteLine();
        }


        public void DisplayDrawResultsForTier(Dictionary<int, (int winningTicketsCount, decimal totalReward)> tierResults, string tierName, decimal rewardPerWinningTicket)
        {
            Console.WriteLine($"* {tierName} - Reward for a winning ticket is {_gameSettings.Currency}{rewardPerWinningTicket}.");
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine($"|   Player    | How many tickets have won |    Total reward    |");
            Console.WriteLine("-----------------------------------------------------------------");

            foreach (var result in tierResults)
            {
                Console.WriteLine($"|    {result.Key,-6}   |             {result.Value.winningTicketsCount,-9}     |       {_gameSettings.Currency}{result.Value.totalReward,-11} |");
            }

            Console.WriteLine("-----------------------------------------------------------------");
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
