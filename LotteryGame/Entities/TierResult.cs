namespace LotteryGame.Entities
{
    public class TierResult
    {
        //Dictionary<int, (int winningTicketsCount, decimal totalReward)>
        public TierBase Tier { get; set; }
        public decimal RewardPerWinningTicket { get; set; }

        public List<Ticket> WinningTickets { get; set; }
    }
}
