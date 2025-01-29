namespace LotteryGame
{
    public class GameSettings
    {
        public decimal TicketPrice { get; set; }
        public string Currency { get; set; }
        public int MaxTicketsPerPlayer { get; set; }
        public int MinTicketsPerPlayer { get; set; }
        public decimal PlayerInitialBalance { get; set; }
        public int MinPlayersPerGame { get; set; }
        public int MaxPlayersPerGame { get; set; }
        public List<Dictionary<string, object>> TierSettings { get; set; }

    }
}
