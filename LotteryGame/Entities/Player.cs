using LotteryGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public double TotalWin { get; set; }
        public List<Ticket> Tickets { get; set; } = new();

        public Player(int id, double initialBalance)
        {
            Id = id;
            Balance = initialBalance;
        }

    }
}
