using LotteryGame.Entities;
using LotteryGame.Interfaces;
using LotteryGame.Services;
using Microsoft.Extensions.Options;
using Moq;

namespace LotteryGame.Test
{
    public class GeneratorServiceTests
    {
        IOptions<GameSettings> GetGameSettingsOptions(GameSettings gameSettings)
        {
            var gameSettingsMock = new Mock<IOptions<GameSettings>>();
            gameSettingsMock.Setup(x => x.Value).Returns(gameSettings);
            return gameSettingsMock.Object;
        }

        [Fact]
        public void ShuffleTickets_GivenEmptyList_RemainsEmptyList()
        {
            //Arrange
            var gameSettings = new GameSettings();
            var gameSettingsOptions = GetGameSettingsOptions(gameSettings);
            var generatorService = new GeneratorService(gameSettingsOptions, Mock.Of<IRandomService>());
            var tickets = new List<Ticket>();

            //Act
            generatorService.ShuffleTickets(tickets);

            //Assert
            Assert.Empty(tickets);
        }

        [Fact]
        public void ShuffleTickets_GivenOneTicketList_RemainsOneTicketListWithSameTicket()
        {
            //Arrange
            var gameSettings = new GameSettings();
            var gameSettingsOptions = GetGameSettingsOptions(gameSettings);
            var generatorService = new GeneratorService(gameSettingsOptions, Mock.Of<IRandomService>());
            var expectedPlayerId = 69;
            var tickets = new List<Ticket> { new Ticket(expectedPlayerId) };

            //Act
            generatorService.ShuffleTickets(tickets);

            //Assert
            Assert.Single(tickets);
            Assert.Equal(expectedPlayerId, tickets[0].PlayerId);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(10, 10)]
        [InlineData(95, 95)]
        [InlineData(100, 100)]
        [InlineData(0, 0)]
        public void ShuffleTickets_GivenTicketsList_DoesntChangeTicketCount(int givenTicketsCount, int expectedTicketsCount)
        {
            //Arrange
            var gameSettings = new GameSettings();
            var gameSettingsOptions = GetGameSettingsOptions(gameSettings);
            var generatorService = new GeneratorService(gameSettingsOptions, Mock.Of<IRandomService>());
            var tickets = new List<Ticket>();

            for(int i = 0; i < givenTicketsCount; i++)
            {
                tickets.Add(new Ticket(i));
            }

            //Act
            generatorService.ShuffleTickets(tickets);

            //Assert
            Assert.Equal(expectedTicketsCount, tickets.Count);
        }


        [Fact]
        public void ShuffleTickets_GivenTicketsList_HasAllOriginalTickets()
        {
            //Arrange
            var gameSettings = new GameSettings();
            var gameSettingsOptions = GetGameSettingsOptions(gameSettings);
            var generatorService = new GeneratorService(gameSettingsOptions, Mock.Of<IRandomService>());
            var originalTickets = new List<Ticket>();

            for (int i = 0; i < 420; i++)
            {
                originalTickets.Add(new Ticket(i));
            }
            var ticketsToShuffle = new List<Ticket>(originalTickets);

            //Act
            generatorService.ShuffleTickets(ticketsToShuffle);

            //Assert
            foreach (var ticket in originalTickets)
            {
                Assert.Contains(ticket, ticketsToShuffle); 
            }
        }

        [Fact]
        public void ShuffleTickets_SetupRandomServiceToSequenceOrder_DoesNotChangesTicketsOrder()
        {
            //Arrange
            var ticketCount = 4;
            var gameSettings = new GameSettings();
            var gameSettingsOptions = GetGameSettingsOptions(gameSettings);
            var randomServiceMock = new Mock<IRandomService>();
            randomServiceMock.SetupSequence(r => r.Generate(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(3)
            .Returns(2)
            .Returns(1)
            .Returns(0);
            var originalTickets = new List<Ticket>();
            for (int i = 0; i < ticketCount; i++)
            {
                originalTickets.Add(new Ticket(i));
            }
            var ticketsToShuffle = new List<Ticket>(originalTickets);

            var generatorService = new GeneratorService(gameSettingsOptions, randomServiceMock.Object);
            
            //Act
            generatorService.ShuffleTickets(ticketsToShuffle);

            //Assert
            for (int i = 0;i < ticketCount; i++) 
            {
                Assert.Equal(originalTickets[i], ticketsToShuffle[i]);
            }
        }

        [Fact]
        public void ShuffleTickets_SetupRandomServiceReturns0_ChangesTicketsOrder()
        {
            //Arrange
            var ticketCount = 4;
            var gameSettings = new GameSettings();
            var gameSettingsOptions = GetGameSettingsOptions(gameSettings);
            var randomServiceMock = new Mock<IRandomService>();
            randomServiceMock.Setup(r => r.Generate(It.IsAny<int>(), It.IsAny<int>())).Returns(0);
            var originalTickets = new List<Ticket>();
            for (int i = 0; i < ticketCount; i++)
            {
                originalTickets.Add(new Ticket(i));
            }
            var ticketsToShuffle = new List<Ticket>(originalTickets);

            var generatorService = new GeneratorService(gameSettingsOptions, randomServiceMock.Object);

            //Act
            generatorService.ShuffleTickets(ticketsToShuffle);

            //Assert
            // 0 1 2 3 => 3 0 1 2
            Assert.Equal(originalTickets[0], ticketsToShuffle[3]);
            Assert.Equal(originalTickets[1], ticketsToShuffle[0]);
            Assert.Equal(originalTickets[2], ticketsToShuffle[1]);
            Assert.Equal(originalTickets[3], ticketsToShuffle[2]);
        }
        
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 100, 1)]
        [InlineData(12, 95, 12)]
        [InlineData(0, 95, 0)]
        [InlineData(95, 95, 95)]
        public void PickWinningTickets_ShouldReturnCorrectNumberOfWinners(int amountToPick, int totalTicketsCount, int expectedResult)
        {
            var gameSettings = new GameSettings();
            var gameSettingsOptions = GetGameSettingsOptions(gameSettings);
            var generatorService = new GeneratorService(gameSettingsOptions, Mock.Of<IRandomService>());
            List<Ticket> allTickets = new();

            for (int i = 0; i < totalTicketsCount; i++)
            {
                Ticket ticket = new(i);
                allTickets.Add(ticket);
            }

            var result = generatorService.PickWinningTickets(amountToPick, allTickets);

            Assert.Equal(expectedResult, result.Count);
            Assert.All(result, ticket => Assert.True(ticket.HasWon));
        }
        
        [Fact]
        public void PickWinningTickets_CountToPickExceedsAvailableTickets_ThrowsException()
        {
            var gameSettings = new GameSettings();
            var gameSettingsOptions = GetGameSettingsOptions(gameSettings);
            var generatorService = new GeneratorService(gameSettingsOptions, Mock.Of<IRandomService>());

            var tickets = new List<Ticket>
            {
                new Ticket(1)
            };

            var ex = Assert.Throws<LotteryException>(() => generatorService.PickWinningTickets(2, tickets));
            Assert.Contains("exceeds the number of available tickets", ex.Message);
        }
        
        [Fact]
        public void PickWinningTickets_ShouldIgnoreAlreadyWinningTickets()
        {
            var gameSettings = new GameSettings();
            var gameSettingsOptions = GetGameSettingsOptions(gameSettings);
            var generatorService = new GeneratorService(gameSettingsOptions, Mock.Of<IRandomService>());

            var tickets = new List<Ticket>
            {
                new Ticket (1)  { HasWon = true },
                new Ticket (1) { HasWon = false },
                new Ticket (2) { HasWon = false },
                new Ticket (2) { HasWon = true },
                new Ticket (3) { HasWon = true },
                new Ticket (4) { HasWon = false },
                new Ticket (5) { HasWon = true },
            };

            var originalIds = tickets.Where(t => !t.HasWon).Select(t => t.Id).ToList();
        
            var result = generatorService.PickWinningTickets(3, tickets);

            Assert.Equal(3, result.Count);
            Assert.All(result, ticket => Assert.True(ticket.HasWon));
            Assert.All(result, ticket => Assert.Contains(ticket.Id, originalIds));
        }

    }
}
