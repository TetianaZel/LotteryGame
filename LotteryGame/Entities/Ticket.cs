using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Entities
{
    public class Ticket
    {
        private  int ticketCounter = 1;

        public int Id { get; private set; }
        public bool HasWon { get; private set; }
        public int PlayerId { get; set; }

        public Ticket(int playerId)
        {
            Id = ticketCounter;
            HasWon = false;
            PlayerId = playerId;

            ticketCounter++;
        }

        private void MarkAsWinner()
        {
            HasWon = true;
        }

    }
}
