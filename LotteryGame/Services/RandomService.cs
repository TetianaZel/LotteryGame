using LotteryGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Services
{
    public class RandomService : IRandomService
    {
        public int Generate(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}
