using LotteryGame;
using LotteryGame.Interfaces;
using LotteryGame.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var serviceProvider = new ServiceCollection()
    .Configure<GameSettings>(configuration)
    .AddSingleton<IRandomService, RandomService>()
    .AddSingleton<LotteryService>()
    .AddSingleton<IGeneratorService, GeneratorService>()
    .AddSingleton<ICalculatorService, CalculatorService>()
    .AddSingleton<IUIManager, UIManager>()
    .AddSingleton<LotteryInitializatorService>()
    .AddSingleton<LotteryApplication>()
    .BuildServiceProvider();

serviceProvider.GetRequiredService<LotteryApplication>().Start();