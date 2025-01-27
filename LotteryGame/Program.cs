using LotteryGame;
using LotteryGame.Interfaces;
using LotteryGame.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;

var serviceProvider = new ServiceCollection()
    .AddSingleton<GameSettings>()
    .AddSingleton<IRandomService, RandomService>()
    .AddSingleton<ILotteryService, LotteryService>()
    .AddSingleton<IGeneratorService, GeneratorService>()
    .AddSingleton<ICalculatorService, CalculatorService>()
    .AddSingleton<IUIManager, UIManager>()
    .BuildServiceProvider();

var game = serviceProvider.GetRequiredService<ILotteryService>();
game.RunLottery();