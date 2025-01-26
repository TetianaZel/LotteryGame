using LotteryGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Interfaces
{
    public interface IGeneratorService
    {
        List<Player> GenerateCpuPlayers(int maxTicketsPlayersCanBuy);

        List<Ticket> GenerateTickets(Player player, int ticketsCount);

        List<Ticket> PickWinningTickets(int count, List<Ticket> allTickets);
    }
}
