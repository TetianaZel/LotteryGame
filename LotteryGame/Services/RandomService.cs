using LotteryGame.Interfaces;

namespace LotteryGame.Services
{
    public class RandomService : IRandomService
    {
        public int Generate(int min, int max)
        {
            return new Random().Next(min, max + 1);
        }
    }
}
