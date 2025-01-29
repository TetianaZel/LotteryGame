namespace LotteryGame.Entities
{
    public class GrandPrizeTier : TierBase
    {
        public int WinningTicketsCount { get; set; }

        public override int GetWinningTicketsCount(int totalTicketsCount)
        {
            return WinningTicketsCount;
        }
    }
}
