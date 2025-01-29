namespace LotteryGame.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public List<Ticket> Tickets { get; set; } = new();

        public Player(int id, decimal initialBalance)
        {
            Id = id;
            Balance = initialBalance;
        }

    }
}
