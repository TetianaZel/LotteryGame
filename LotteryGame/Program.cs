using LotteryGame;
using LotteryGame.Interfaces;
using LotteryGame.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;

var serviceProvider = new ServiceCollection()
    .AddSingleton<GameSettings>()
    .AddSingleton<IRandomService, RandomService>()
    .AddSingleton<ILotteryService, LotteryService>()
    .AddSingleton<ILotteryCpuGeneratorService, LotteryCpuGeneratorService>()
    .AddSingleton<ILotteryCalculatorService, LotteryCalculatorService>()
    .AddSingleton<IUIManager, UIManager>()
    .BuildServiceProvider();

var game = serviceProvider.GetRequiredService<IUIManager>();
game.ShowGreetingMessage(10);
game.GetPlayerInputTickets(10);