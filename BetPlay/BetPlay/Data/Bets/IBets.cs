using Microsoft.AspNetCore.Http;

namespace BetPlay.Data.Bets
{
    public interface IBets
    {
        bool LoadBet(Entities.Bets bet);
        decimal MoneyByRoulette(int rouletteId);
    }
}
