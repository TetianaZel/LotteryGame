using System.Runtime.Serialization;

namespace LotteryGame
{
    [Serializable]
    public class LotteryException : Exception
    {
        public LotteryException()
        {
        }

        public LotteryException(string? message) : base(message)
        {
        }
       
    }
}