using LotteryGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Interfaces
{
    public interface ILotteryCpuGeneratorService
    {
        List<Player> GenerateCpuPlayers(int maxTicketsPlayersCanBuy);

        List<Ticket> GenerateTickets(int playerId, int ticketsCount);
    }
}
