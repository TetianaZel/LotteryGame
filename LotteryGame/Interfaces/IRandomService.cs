using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Interfaces
{
    public interface IRandomService
    {
        int Generate(int min, int max);
    }
}
