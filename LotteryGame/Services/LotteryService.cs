using LotteryGame.Entities;
using LotteryGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Services
{
    public class LotteryService : ILotteryService
    {
        private readonly GameSettings _gameSettings;
        private readonly ILotteryCpuGeneratorService _generator;
        private readonly IUIManager _uiManager;

        public LotteryService(GameSettings gameSettings, ILotteryCpuGeneratorService generator, IUIManager uiManager)
        {
            _gameSettings = gameSettings;
            _generator = generator;
            _uiManager = uiManager;
        }
        public void RunLottery()
        {
            //VALIDATE LOTTERY SETTINGS

            if (_gameSettings.GrandPrizeRevenueShare + _gameSettings.SecondTierRevenueShare + _gameSettings.ThirdTierRevenueShare >= 1)
            {
                throw new Exception("Invalid revenue prize tier shares in the settings! Not profitable for the house!");
            }

            //max tickets each player can buy
            int maxTicketsAffordable = (int)Math.Floor(_gameSettings.PlayerInitialBalance / _gameSettings.TicketPrice);

            if (maxTicketsAffordable < _gameSettings.MinTicketsPerPlayer)
            {
                throw new Exception("Players can't afford to buy minimum required amount of tickets. Ticket price is too high for the players' initial balance.");
            }

            int maxTicketsPlayerCanBuy = Math.Min(maxTicketsAffordable, _gameSettings.MaxTicketsPerPlayer);

            int ticketsToBuy = _uiManager.GetPlayerInputTickets(maxTicketsPlayerCanBuy);

            Player player1 = new Player(1, _gameSettings.PlayerInitialBalance);

            List<Ticket> listTickets = PurchaseTickets(ticketsToBuy, player1);

            var cpuPlayers = _generator.GenerateCpuPlayers(maxTicketsPlayerCanBuy);

            //add player 1 to list of cpu to define total amount of players
            //add lists of tickets together
        }


        public List<Ticket> PurchaseTickets(int count, Player player)
        {
            List<Ticket> tickets = new List<Ticket>();

            for (int i = 0; i < count; i++) //check
            {
                tickets.Add(new Ticket(player.Id));
            }

            UpdatePlayerBalance(player, count * _gameSettings.TicketPrice);

            return tickets;
        }

        public void UpdatePlayerBalance(Player player, double balanceChange)
        {
            if (player != null)
            {
                player.Balance += balanceChange;
            }
        }



    }
}
