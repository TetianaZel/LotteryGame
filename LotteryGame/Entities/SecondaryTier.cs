namespace LotteryGame.Entities
{
    public class SecondaryTier : TierBase
    {
        public decimal WinningTicketsShare { get; set; }

        public override int GetWinningTicketsCount(int totalTicketsCount)
        {
            return (int)Math.Floor(totalTicketsCount * WinningTicketsShare);
        }
    }
}
