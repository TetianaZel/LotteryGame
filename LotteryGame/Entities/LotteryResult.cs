namespace LotteryGame.Entities
{
    public class LotteryResult
    {
        public List<TierResult> TierResults { get; set; } = new();
        public decimal HouseProfit { get; set; }
    }
}
