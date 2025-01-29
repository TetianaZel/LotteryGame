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

        public LotteryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LotteryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}