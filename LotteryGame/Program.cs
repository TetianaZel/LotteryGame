using LotteryGame;
using LotteryGame.Interfaces;
using LotteryGame.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var serviceProvider = new ServiceCollection()
    .Configure<GameSettings>(configuration)
    .AddSingleton<IRandomService, RandomService>()
    .AddSingleton<LotteryService>()
    .AddSingleton<IGeneratorService, GeneratorService>()
    .AddSingleton<ICalculatorService, CalculatorService>()
    .AddSingleton<IUIManager, UIManager>()
    .BuildServiceProvider();

//var settings = serviceProvider.GetRequiredService<IOptions<GameSettings>>();

var game = serviceProvider.GetRequiredService<LotteryService>();
game.RunLottery();