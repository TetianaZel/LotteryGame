namespace LotteryGame.Entities
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public bool HasWon { get; set; }
        public int PlayerId { get; set; }
        public decimal Price { get; set; }

        public Ticket(int playerId)
        {
            Id = Guid.NewGuid();
            PlayerId = playerId;
        }
    }
}
