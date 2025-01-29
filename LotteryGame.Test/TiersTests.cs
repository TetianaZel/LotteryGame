using LotteryGame.Entities;

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
        public void GetWinningTicketsCount_GivenTotalTicketsCount_ReturnsCorrectWinningTicketsAmount(int totalTicketsCount, int expectedResult)
        {
            //Arrange
            var tier = new SecondaryTier { Name = "AnySecondaryTier", RevenueShare = 0.3m, WinningTicketsShare = 0.3m};

            //Act
            var result = tier.GetWinningTicketsCount(totalTicketsCount);

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1, 100)]
        [InlineData(0, 95)]
        [InlineData(2, 10)]
        [InlineData(10, 10)]
        public void GetWinningTicketsCount_GrandPrizeTier_ReturnsExpectedCount(int expectedWinningTicketsCount, int totalTicketsCount)
        {
            //Arrange
            var grandPrizeTier = new GrandPrizeTier { Name = "GrandPrizeTier", RevenueShare = 0.5m, WinningTicketsCount = expectedWinningTicketsCount };

            //Act
            var result = grandPrizeTier.GetWinningTicketsCount(totalTicketsCount);

            //Assert
            Assert.Equal(expectedWinningTicketsCount, result);
        }
    }
}
