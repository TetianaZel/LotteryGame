namespace LotteryGame.Entities
{
    public abstract class TierBase
    {
        public string Name { get; set; }
        public decimal RevenueShare { get; set; }
        public abstract int GetWinningTicketsCount(int totalTicketsCount);
    }
}
