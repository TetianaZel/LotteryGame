using LotteryGame.Entities;
using LotteryGame.Enums;
using LotteryGame.Interfaces;
using LotteryGame.Services;

namespace LotteryGame.Test
{
    public class CalculatorServiceTests
    {
        [Fact]
        public void CalculateTotalRevenue_NoPlayers_ReturnsZero()
        {
            //Arrange
            var gameSettings = new GameSettings();
            var calculatorService = new CalculatorService(gameSettings);
            var players = new List<Player>();

            //Act
            var result = calculatorService.CalculateTotalRevenue(players);

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void CalculateTotalRevenue_OnePlayerOneTicket_ReturnsOneTicketPrice() //fix
        {
            //Arrange
            var gameSettings = new GameSettings();
            var calculatorService = new CalculatorService(gameSettings);
            var player = new Player(2, gameSettings.PlayerInitialBalance);
            player.Tickets = new List<Ticket>() { new(player.Id) };
            var players = new List<Player>{player};

            //Act
            var result = calculatorService.CalculateTotalRevenue(players);

            //Assert
            Assert.Equal(gameSettings.TicketPrice, result);
        }

        [Fact]
        public void CalculateTotalRevenue_FivePlayersThreeTicketsEach_ReturnsCorrectTotalRevenue()
        {
            //Arrange
            var gameSettings = new GameSettings();
            var calculatorService = new CalculatorService(gameSettings);
            var players = new List<Player>();
            for (int i = 0; i < 5; i++)
            {
                var player = new Player(i, gameSettings.PlayerInitialBalance);
                player.Tickets = Enumerable.Range(0, 3).Select(x => new Ticket(player.Id)).ToList();
                players.Add(player);
            }

            //Act
            var result = calculatorService.CalculateTotalRevenue(players);

            //Assert
            var expectedRevenue = gameSettings.TicketPrice * 5 * 3; // 5 players, 3 tickets each

            Assert.Equal(expectedRevenue, result);
        }

        [Fact]
        public void CalculateTierRevenue_ZeroTotalRevenue_ZeroTierRevenue()
        {
            //Arrange
            var gameSettings = new GameSettings();
            var calculatorService = new CalculatorService(gameSettings);
            var totalRevenue = 0;
            var tier = new Tier(PrizeTier.SecondTier, 0.1m, 0.3m);

            //Act
            var result = calculatorService.CalculateTierRevenue(tier, totalRevenue);

            //Assert
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(100, 0.1, 10)]
        [InlineData(90, 0.2, 18)]
        [InlineData(95, 0.3, 28.5)]
        [InlineData(103, 0.25, 25.75)]
        [InlineData(91, 0.3, 27.3)]
        public void CalculateTierRevenue_GivenTotalRevenue_ReturnsCorrectTierRevenue(int totalRevenue, decimal revenueShare, decimal expectedResult)
        {
            //Arrange
            var gameSettings = new GameSettings();
            var calculatorService = new CalculatorService(gameSettings);
            var tier = new Tier(PrizeTier.SecondTier, revenueShare, 0.3m);

            //Act
            var result = calculatorService.CalculateTierRevenue(tier, totalRevenue);

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(0, 1, 0)]
        [InlineData(50, 1, 50)]
        [InlineData(15, 7, 2.14)]
        public void CalculateRewardPerWinningTicket_GivenTierRevenueAndWinningTicketsCount_ReturnsCorrectReward(decimal revenue, int winningTicketsCount, decimal expectedResult)
        {
            //Arrange
            var gameSettings = new GameSettings();
            var calculatorService = new CalculatorService(gameSettings);

            //Act
            var result = calculatorService.CalculateRewardPerWinningTicket(revenue, winningTicketsCount);

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void CalculateRewardPerWinningTicket_WinningTicketsCountZero_ThrowsException()
        {
            //Arrange
            var gameSettings = new GameSettings();
            var calculatorService = new CalculatorService(gameSettings);

            //Act + Assert
            Assert.Throws<Exception>(() => { calculatorService.CalculateRewardPerWinningTicket(100, 0); });
        }

        [Theory]
        [InlineData(0, 10, 0)]
        [InlineData(1, 10, 10)]
        [InlineData(1.25, 7, 8.75)]
        [InlineData(3.33, 7, 23.31)]
        [InlineData(50, 1, 50)]
        public void CalculateTierDistributedRevenue_GivenRewardPerWinnerAndTicketsCount_ReturnsCorrectResult(decimal rewardPerWinner, int winningTicketsCount, decimal expectedResult)
        {
            //Arrange
            var gameSettings = new GameSettings();
            var calculatorService = new CalculatorService(gameSettings);

            //Act
            var result = calculatorService.CalculateTierDistributedRevenue(rewardPerWinner, winningTicketsCount);

            //Assert
            Assert.Equal(expectedResult, result);
        }
    }
}