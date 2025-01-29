using System.Runtime.Serialization;

namespace LotteryGame
{
    [Serializable]
    internal class LotteryException : Exception
    {
        public LotteryException()
        {
        }

        public LotteryException(string? message) : base(message)
        {
        }
       
    }
}