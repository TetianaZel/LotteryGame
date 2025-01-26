using LotteryGame.Entities;
using LotteryGame.Enums;
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
        private readonly IGeneratorService _generator;
        private readonly IUIManager _uiManager;
        private readonly ICalculatorService _calculator;

        public LotteryService(GameSettings gameSettings, IGeneratorService generator, IUIManager uiManager, ICalculatorService calculator)
        {
            _gameSettings = gameSettings;
            _generator = generator;
            _uiManager = uiManager;
            _calculator = calculator;
        }
        public void RunLottery()
        {
            //VALIDATE LOTTERY SETTINGS
            int maxTicketsAffordableByBalance = (int)Math.Floor(_gameSettings.PlayerInitialBalance / _gameSettings.TicketPrice);

            ValidateGameSettings(maxTicketsAffordableByBalance);
            var bank = InitializeBank();

            List<Tier> tiers = InitializeTiers();

            int maxTicketsPlayerCanBuy = GetUpperLimitTicketsToBuy(maxTicketsAffordableByBalance);

            Player player1 = InitializePlayer1(maxTicketsPlayerCanBuy);

            var players = _generator.GenerateCpuPlayers(maxTicketsPlayerCanBuy);

            players.Add(player1);

            _uiManager.DisplayAllPlayersAndPurchases();

            List<Ticket> allTickets = new List<Ticket>();

            foreach (Player player in players)
            {
                allTickets.AddRange(player.Tickets);
            }

            bank.TotalRevenue = _calculator.CalculateTotalRevenue(players);

            foreach (Tier tier in tiers)
            {
                tier.TierRevenue = _calculator.CalculateTierRevenue(tier, bank.TotalRevenue);

                tier.WinningTicketsNumber = _calculator.CalculateWinningTicketsNumber(tier, allTickets.Count);

                List<Ticket> winningTickets = _generator.PickWinningTickets(tier.WinningTicketsNumber, allTickets);

                //get list of players from tickets and update in Tier

                tier.RewardPerWinner = _calculator.CalculateRewardPerWinner(tier.TierRevenue, tier.WinningTicketsNumber);

                tier.TierDistributedRevenue = _calculator.CalculateTierDistributedRevenue(tier.RewardPerWinner, tier.WinningTicketsNumber);
            }
        }

        private Bank InitializeBank()
        {
            return new Bank();
        }

        private List<Tier> InitializeTiers()
        {
            return new List<Tier>
            {
                new Tier(PrizeTier.GrandPrize, _gameSettings.GrandPrizeRevenueShare, _gameSettings.GrandPrizeWinningTickets),
                new Tier(PrizeTier.SecondTier, _gameSettings.SecondTierRevenueShare, _gameSettings.SecondTierWinningTicketsShare),
                new Tier(PrizeTier.ThirdTier, _gameSettings.ThirdTierRevenueShare, _gameSettings.ThirdTierWinningTicketsShare)
            };
        }

        private Player InitializePlayer1(int maxTicketsPlayerCanBuy)
        {
            int ticketsToBuy = _uiManager.GetPlayerInputTickets(maxTicketsPlayerCanBuy);

            Player player1 = new Player(1, _gameSettings.PlayerInitialBalance);

            List<Ticket> listTicketsForPlayer1 = PurchaseTickets(ticketsToBuy, player1);

            player1.Tickets = listTicketsForPlayer1;
            return player1;
        }

        private int GetUpperLimitTicketsToBuy(int maxTicketsAffordable)
        {
            return Math.Min(maxTicketsAffordable, _gameSettings.MaxTicketsPerPlayer);
        }

        private void ValidateGameSettings(int maxTicketsAffordable)
        {
            if (_gameSettings.GrandPrizeRevenueShare + _gameSettings.SecondTierRevenueShare + _gameSettings.ThirdTierRevenueShare >= 1)
            {
                throw new Exception("Invalid revenue prize tier shares in the settings! Not profitable for the house!");
            }

            if (maxTicketsAffordable < _gameSettings.MinTicketsPerPlayer)
            {
                throw new Exception("Players can't afford to buy minimum required amount of tickets. Ticket price is too high for the players' initial balance.");
            }
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

        public void UpdatePlayerBalance(Player player, decimal balanceChange)
        {
            if (player != null)
            {
                player.Balance += balanceChange;
            }
        }



    }
}
