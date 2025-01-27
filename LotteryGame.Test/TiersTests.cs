using LotteryGame.Entities;
using LotteryGame.Enums;
using LotteryGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Test
{
    public class TiersTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(100, 30)]
        [InlineData(90, 27)]
        [InlineData(95, 28)]
        [InlineData(87, 26)]
        [InlineData(81, 24)]
        public void GetWinningTicketsNumber_GivenTotalTicketsCount_ReturnsCorrectWinningTicketsAmount(int totalTicketsCount, int expectedResult)
        {
            //Arrange
            var tier = new Tier(PrizeTier.SecondTier, 0.3m, 0.3m);

            //Act
            var result = tier.GetWinningTicketsNumber(totalTicketsCount);

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GetWinningTicketsNumber_GrandPrizeTier_ReturnsTicketsNumberFromSettings()
        {
            //Arrange
            var gameSettings = new GameSettings();
            var grandPrizeTier = new GrandPrizeTier(gameSettings.GrandPrizeRevenueShare, gameSettings.GrandPrizeWinningTickets);

            //Act
            var result = grandPrizeTier.GetWinningTicketsNumber(50);

            //Assert
            Assert.Equal(gameSettings.GrandPrizeWinningTickets, result);
        }
    }
}
