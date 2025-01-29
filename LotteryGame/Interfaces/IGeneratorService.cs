using LotteryGame.Entities;

namespace LotteryGame.Interfaces
{
    public interface IGeneratorService
    {
        List<Player> GenerateCpuPlayers(int maxTicketsPlayersCanBuy);

        List<Ticket> GenerateTickets(int ticketsCount, Player player);

        List<Ticket> PickWinningTickets(int count, List<Ticket> allTickets);

        void ShuffleTickets(List<Ticket> allTickets);
    }
}
